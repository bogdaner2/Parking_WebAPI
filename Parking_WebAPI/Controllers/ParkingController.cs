using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Parking.Data;

namespace Parking_WebAPI.Controllers
{
    [Route("api/[controller]/")]
    public class ParkingController : Controller
    {
        private readonly Parking.Data.Parking parking = Parking.Data.Parking.Instance;

        public string Info()
        {
            return "|GET|  Show cars      : \"/api/parking/showcars\" \n" +  //
                   "|GET|  Show car by id : \"/api/parking/showcars/{id}\" \n" + //
                   "|GET|  Show free spots : \"/api/parking/showcars/{id}\" \n" + //
                   "|GET|  Show occupied spots : \"/api/parking/showcars/{id}\" \n" + //
                   "|GET|  Show transaction history for the last minute\n" +
                   "|GET|  Show transaction history for the last minute for certain car\n" + //
                   "|GET|  Show Transcations.log :\n" + //
                   "|GET|  Show parking balance :\n" + //
                   "|POST| Add car        : \"/api/parking/showcars/{id}\"\n" +
                   "|PUT|  Recharge car balance\n" +
                   "|DELETE| Remove car ";
        }

        [HttpGet("[action]")]
        public async Task<string> ShowBalance() => 
            await Task.Run(() => JsonConvert.SerializeObject(parking.Balance));

        [HttpGet("[action]")]
        public async Task<string> FreeSpots() =>
            await Task.Run(() => JsonConvert.SerializeObject(parking.ShowFreeSpots()));

        [HttpGet("[action]/{id}")]
        public async Task<string> ShowTransactionsForCar(int id) =>
            await Task.Run(() => JsonConvert.SerializeObject(Transaction.TransactionsForCurtainCar(id,parking)));

        [HttpGet("[action]")]
        public async Task<string> OccupiedSpots() =>
            await Task.Run(() => JsonConvert.SerializeObject(parking.ShowOccupiedSpots()));

        [HttpGet("[action]")]
        public async Task<string> ShowCars() =>
            await Task.Run(() => JsonConvert.SerializeObject(parking.Cars));

        [HttpGet("[action]")]
        public async Task<string> ShowLog()
        {
            try
            {
                using (StreamReader stream = new StreamReader("Transactions.log"))
                {
                    return await stream.ReadToEndAsync();
                }
            }
            catch (Exception e) { throw new Exception("File doesnt exist right now.Please,Wait for the first transaction"); }
        }

        [HttpGet("[action]/{id}")]
        public async Task<string> ShowCars(int id)
        {
                var car = parking.Cars[id];
                return await Task.Run(() => JsonConvert.SerializeObject(car));
        }


        [Route("[action]/{id}&{balance}")]
        [HttpPut]
        public async Task<string> RechargeBalance(int id, int balance)
        {
            var car = parking.Cars[id];
            car.RechargeBalance(balance);
            return await Task.Run(() => "Car id:" + car.Id + "balance = " + car.CarBalance);
        }
        [Route("[action]/{type}&{balance}")]
        [HttpPost]
        public async Task<string> AddCar(int balance,int type)
        {
            var car = new Car(balance, (Car.CarType) type);
            parking.Cars.Add(car);
            return await Task.Run(() => "Car id:" + car.Id + " was added");
        }
        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<string> RemoveCar(int id)
        {
            var car = parking.Cars[id];
            parking.Cars.Remove(car);
            return await Task.Run(() => "Car id:" + car.Id + "was removed");
        }
    }
}