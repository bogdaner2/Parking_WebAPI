using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Parking_WebAPI.Controllers
{
    [Route("api/[controller]/")]
    public class ParkingController : Controller
    {
        private readonly Parking.Data.Parking parking = Parking.Data.Parking.Instance;

        public string Info()
        {
            return "|GET|  Show cars      : \"/api/car/cars\" \n" +  
                   "|GET|  Show car by id : \"/api/car/cars/{id}\" \n" + 
                   "|GET|  Show free spots : \"/api/parking/freespots\" \n" + 
                   "|GET|  Show occupied spots : \"/api/parking/occupiedspots\" \n" +
                   "|GET|  Show transaction history for the last minute : /api/transaction/last_minute_transactions\n" +
                   "|GET|  Show transaction history for the last minute for certain car : \"/api/transaction/last_minute_transactions/{id}\"\n" + 
                   "|GET|  Show Transcations.log : \"/api/transaction/log\"\n" +
                   "|GET|  Show parking balance : \"/api/parking/balance\"\n" + 
                   "|POST| Add car        : \"/api/car/add_car/{type}&{balance:int}\"\n" +
                   " type = 1 Passenger | type = 2 Truck | type = 3 Bus | type = 4 Motorcycle\n" +
                   "|PUT|  Recharge car balance : \"/api/transaction/recharge_balance/{id}&{balance:int}\"\n" +
                   "|DELETE| Remove car : \"/api/car/remove_car/{id}\"";
        }

        [HttpGet("[action]")]
        public async Task<string> Balance() => 
            await Task.FromResult(JsonConvert.SerializeObject(parking.Balance));

        [HttpGet("[action]")]
        public async Task<string> FreeSpots() =>
            await Task.FromResult(JsonConvert.SerializeObject(parking.ShowFreeSpots()));


        [HttpGet("[action]")]
        public async Task<string> OccupiedSpots() =>
            await Task.FromResult(JsonConvert.SerializeObject(parking.ShowOccupiedSpots()));

    }
}