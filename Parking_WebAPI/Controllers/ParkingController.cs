using System.Collections;
using System.Collections.Generic;
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
                   "|GET|  Show Transcations.log :\n" +
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

        [HttpGet("[action]/{id}")]
        public async Task<string> ShowCars(int id)
        {
                var car = parking.Cars[id];
                return await Task.Run(() => JsonConvert.SerializeObject(car));
        }


        [Route("[action]/{id}&{balance}")]
        [HttpPut]
        public string RechargeBalance(int balance,int id)
        {
            parking.Cars[id].RechargeBalance(balance);
            return "Car Added";
        }
        [Route("[action]/{id}&{balance}")]
        [HttpPost]
        public string AddCar(int balance, int id)
        {
            parking.Cars[id].RechargeBalance(balance);
            return "Car Added";
        }
        [Route("[action]/{id}&{balance}")]
        [HttpDelete]
        public string RemoveCar(int balance, int id)
        {
            parking.Cars[id].RechargeBalance(balance);
            return "Car Added";
        }
    }
}