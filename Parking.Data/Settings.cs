using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Parking.Data
{
    public class Settings
    {
        IConfiguration Configuration { get; }
        public int Timeout { get; }
        public Dictionary<Car.CarType, int> Prices { get;  }
        public int ParkingPlace { get; }
        public double Fine { get;  }
        private static readonly Lazy<Settings> Lazy = new Lazy<Settings>(() => new Settings());
        public static Settings Instance => Lazy.Value;

        private Settings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("ParkingSettings.json");
            Configuration = builder.Build();
            ParkingPlace = int.Parse(Configuration["parkingPlace"]);
            Timeout = int.Parse(Configuration["timeout"]);
            Fine = double.Parse(Configuration["fine"], CultureInfo.InvariantCulture);
            var _prices = new Dictionary<Car.CarType,int>();
            foreach (var child in Configuration.GetSection("prices").GetChildren())
            {
                _prices.Add(
                    (Car.CarType)Enum.Parse(typeof(Car.CarType), child.Key),
                    int.Parse(child.Value));
            }
            Prices = _prices;
        }

    }
}
