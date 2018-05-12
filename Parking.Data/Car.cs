﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Parking.Data
{
    public class Car
    {
        //private static int counter = 1;
        public int Id { get; set; }
        public double CarBalance { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public CarType TypeOfTransport { get ; set; }

        public Car(int balance,CarType type)
        {
            //Id = counter;
            //counter++;
            CarBalance = balance;
            TypeOfTransport = type;
        }

        public Car() { }

        public enum CarType
        {
            Passenger,
            Truck,
            Bus,
            Motorcycle
        }

        public void RechargeBalance(int count)
        {
            CarBalance += count;
        }

        public void Withdraw(double count)
        {
            CarBalance -= count;
        }

        public override string ToString()
        {
            string id = Id.ToString().Substring(Id.ToString().Length - 5);
            return string.Format($"Id:{id} Balance:{CarBalance} Type {TypeOfTransport}");
        }
    }
}
