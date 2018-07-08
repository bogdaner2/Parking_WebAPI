using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Parking.Data
{
    public class Parking
    {
        public delegate void TransactionsRefreshHandler();
        public event TransactionsRefreshHandler OnAddTransaction;

        private static readonly Lazy<Parking> Lazy = new Lazy<Parking>(() => new Parking());
        public static Parking Instance => Lazy.Value;
        public List<Car> Cars { get; set; }
        public List<Transaction> Transactions { get; }
        public double Balance { get; set; }
        public Settings Settings { get; }

        private Parking()
        {
            Cars = new List<Car>();
            Transactions = new List<Transaction>();
            OnAddTransaction += Refresh;
            Balance = 0;
            Settings = Settings.Instance;
        }

        public void AddTransaction(int id , double fee)
        {
            Transactions.Add(new Transaction(id,fee));
            OnAddTransaction?.Invoke();
        }
        private void Refresh()
        {
            Transactions.RemoveAll(x => (DateTime.Now - x.Time).TotalMinutes > 1);
        }
        public void ChargeAFee(Parking parking, ref double earned)
        {
            foreach (var car in parking.Cars)
            {
                double fee;
                if (car.CarBalance > 0)
                {
                    fee = parking.Settings.Prices[car.TypeOfTransport];
                    car.Withdraw(fee);
                }
                else
                {
                    fee = parking.Settings.Prices[car.TypeOfTransport] * parking.Settings.Fine;
                    car.Withdraw(fee);
                }
                parking.Balance += fee;
                earned += fee;
                parking.AddTransaction(car.Id, fee);
            }
        }
        public void Log(ref double earnedAmount, ref bool firstTick)
        {
            if (firstTick != true)
            {
                using (StreamWriter streamWriter = File.AppendText("Transactions.log"))
                {
                    streamWriter.WriteLine("Parking earned : {0} ", earnedAmount);
                    streamWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                        DateTime.Now.ToLongDateString());
                    streamWriter.WriteLine("-------------------------------");
                }

                earnedAmount = 0;
            }
            firstTick = false;
        }
        public void LoadCars()
        {
            using (StreamReader file = new StreamReader("Cars.json"))
            {
                Cars = JsonConvert.DeserializeObject<List<Car>>(file.ReadToEnd());
            }
        }
        public async Task<string> AddCar(int balance, int type)
        {
            if (type < 1 || type > 4) { return await Task.Run(() => "Please, input right type"); }
            if (Cars.Count == Settings.ParkingPlace) { return await Task.Run(() => "No such car"); }
            var car = new Car(balance, (Car.CarType)type);
            Cars.Add(car);
            Cars = Cars.OrderBy(c => c.Id).ToList();
            WriteJson();
            return await Task.FromResult("Car id:" + car.Id + " was added");
        }
        public async Task<string> RemoveCar(int id)
        {
            var car = Cars.Find(x => x.Id == id);
            if (car == null) { return await Task.Run(() => "No such car"); }
            if (car.CarBalance < 0) { return await Task.Run(() => "Please,recharge balance and try again"); }
            Cars.Remove(car);
            WriteJson();
            return await Task.Run(() => "Car was removed");
        }
        public async Task<string> RechargeBalance(int id, int balance)
        {
            var car = Cars.Find(x => x.Id == id);
            if (car == null)
            {
                return await Task.Run(() => "There no such car");
            }
            car.RechargeBalance(balance);
            WriteJson();
            return await Task.Run(() => "Car id:" + car.Id + "balance = " + car.CarBalance);
        }
        public async Task<string> ShowLog()
        {
            try
            {
                using (StreamReader stream = new StreamReader("Transactions.log"))
                {
                    return await stream.ReadToEndAsync();
                }
            }
            catch (Exception) { return await Task.Run(()=>"File doesnt exist right now.Please,Wait for the first transaction"); }
        }
        public int ShowFreeSpots() => Settings.ParkingPlace - Cars.Count;
        public int ShowOccupiedSpots() => Cars.Count;
        private void WriteJson()
        {
            using (StreamWriter file = new StreamWriter("Cars.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, Cars);
            }
        }
    }
}
