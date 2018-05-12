using System;
using System.IO;

namespace Parking.Data
{
    internal static class Menu
    {
        private static readonly Parking Parking = Parking.Instance;

        private static void ShowLastMinuteLog_Menu()
        {
            foreach (var transaction in Parking.Transactions)
            {
                Console.WriteLine(transaction);
            }
        }
        private static void ShowLog_Menu()
        {
            using (StreamReader stream = new StreamReader("Transactions.log"))
            {
                var log = stream.ReadToEnd();
                Console.WriteLine(log);
            }
        }
        private static void ShowBalance_Menu()
        {
            Console.WriteLine("Parking balance: {0:N2} $" , Parking.Balance);
        }
        private static void AddCar_Menu()
        {
            Console.WriteLine("Select car type:\n" +
                              "1)Passenger\n" +
                              "2)Truck \n" +
                              "3)Bus \n" +
                              "4)Motorcycle");
            int.TryParse(Console.ReadLine(), out int type);
            if(type < 1 || type > 4) { throw new Exception("Inccorect data.Please,try again!"); }
            Console.WriteLine("Input balance");
            int.TryParse(Console.ReadLine(), out int balance);
            if (Parking.Settings.ParkingPlace > Parking.Cars.Count)
            {
                var car = new Car(balance,
                    (Car.CarType)Enum.Parse(typeof(Car.CarType),
                        (type - 1).ToString()));
                Parking.Cars.Add(car);
                Console.WriteLine("Car id: " +
                                  car.Id.ToString()
                                      .Substring(car.Id.ToString().Length - 5) +
                                  " was added");
            }
            else
            {
                Console.WriteLine("Maximum number of seats occupied");
            }
        }
        private static void RemoveCar_Menu()
        {
            Console.WriteLine("Select car number for remove:");
            ShowCars_Menu();
            CarSelection(out Car chosenCar);
            if (chosenCar.CarBalance < 0)
            {
                throw new Exception("Car balance is insufficient.Please recharge balance and try again");
            }
            Parking.Cars.Remove(chosenCar);
            Console.WriteLine("Car id: " +
                              chosenCar.Id.ToString()
                              .Substring(chosenCar.Id.ToString().Length - 5) +
                              " was removed");
        }
        private static void RechargeBalance_Menu()
        {
            Console.WriteLine("Choose car balance for recharging :");
            ShowCars_Menu();
            CarSelection(out Car chosenCar);
            Console.WriteLine("Input the amount of the replenishment");
            int.TryParse(Console.ReadLine(), out int amount);
            chosenCar.RechargeBalance(amount);
        }
        private static void ShowFreeSpots_Menu()
        {
            Console.WriteLine(
                string.Format($"On parking {Parking.Cars.Count} cars | " +
                              $"Free spots : {Parking.Settings.ParkingPlace - Parking.Cars.Count}"));
        }
        private static void ShowCars_Menu()
        {
            int itterator = 0;
            foreach (var car in Parking.Cars)
            {
                itterator++;
                Console.WriteLine($"{itterator}){car}");
            }
        }
        private static void CarSelection(out Car chosenCar)
        {
            int.TryParse(Console.ReadLine(), out int choise);
            if (choise <= 0 || choise > Parking.Cars.Count) { throw new Exception("There is no such number of сar.Please,try again"); }
            chosenCar = Parking.Cars[choise - 1];
        }
    }
}
