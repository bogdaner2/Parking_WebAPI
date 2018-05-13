using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Parking.Data
{
    public class Car
    {
        public int Id { get; set; }
        public double CarBalance { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public CarType TypeOfTransport { get ; set; }

        public Car(int balance,CarType type)
        {
            Id = GetUniqueId();
            CarBalance = balance;
            TypeOfTransport = type;
        }

        public enum CarType
        {
            Passenger,
            Truck,
            Bus,
            Motorcycle
        }

        private static int GetUniqueId()
        {
            //Provide distinct values of Id when you add new car after removing previous
            var cars = Parking.Instance.Cars;
            var min = 1;
            var max = Parking.Instance.Settings.ParkingPlace + 1;
            var nums = Enumerable.Range(min,max);
            foreach (var num in nums)
            {
                if (cars.Find(x => x.Id == num) == null)
                {
                    return num;
                }
            }
            return 0;
        }

        public void RechargeBalance(int count)
        {
            CarBalance += count;
        }

        public void Withdraw(double count)
        {
            CarBalance -= count;
        }
    }
}
