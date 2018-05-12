using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

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
            Cars.Add(new Car(100, Car.CarType.Motorcycle));
            Cars.Add(new Car(100, Car.CarType.Motorcycle));
            Cars.Add(new Car(100, Car.CarType.Motorcycle));
            Cars.Add(new Car(100, Car.CarType.Motorcycle));
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
        public void SaveCars()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Car>));
            using (FileStream fs = new FileStream("Cars.xml", FileMode.Create))
            {
                serializer.Serialize(fs, Cars);
            }
        }
        public void LoadCars()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Car>));
                using (FileStream fs = new FileStream("Cars.xml", FileMode.OpenOrCreate))
                {
                    Cars = (List<Car>) serializer.Deserialize(fs);
                }
            }
            catch(Exception e) { throw new Exception("Сant load cars database.Maybe,the file was corrupted");}
        }
    }
}
