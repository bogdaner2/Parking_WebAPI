using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            return "|GET|  Show cars      : \"/api/parking/cars\" \n" +  
                   "|GET|  Show car by id : \"/api/parking/cars/{id}\" \n" + 
                   "|GET|  Show free spots : \"/api/parking/freespots\" \n" + 
                   "|GET|  Show occupied spots : \"/api/parking/occupiedspots\" \n" +
                   "|GET|  Show transaction history for the last minute : /api/parking/last_minute_transactions\n" +
                   "|GET|  Show transaction history for the last minute for certain car : \"/api/parking/last_minute_transactions/{id}\"\n" + 
                   "|GET|  Show Transcations.log : \"/api/parking/log\"\n" +
                   "|GET|  Show parking balance : \"/api/parking/balance\"\n" + 
                   "|POST| Add car        : \"/api/parking/add_car/{type}&{balance:int}\"\n" +
                   " type = 1 Passenger | type = 2 Truck | type = 3 Bus | type = 4 Motorcycle\n" +
                   "|PUT|  Recharge car balance : \"/api/parking/recharge_balance/{id}&{balance:int}\"\n" +
                   "|DELETE| Remove car : \"/api/parking/remove_car/{id}\"";
        }

        [HttpGet("[action]")]
        public async Task<string> Balance() => 
            await Task.Run(() => JsonConvert.SerializeObject(parking.Balance));

        [HttpGet("[action]")]
        public async Task<string> FreeSpots() =>
            await Task.Run(() => JsonConvert.SerializeObject(parking.ShowFreeSpots()));

        [HttpGet("[action]/{id}")]
        public async Task<string> Last_Minute_Transactions(int id) =>
            await Task.Run(() => JsonConvert.SerializeObject(Transaction.TransactionsForCurtainCar(id,parking)));

        [HttpGet("[action]")]
        public async Task<string> OccupiedSpots() =>
            await Task.Run(() => JsonConvert.SerializeObject(parking.ShowOccupiedSpots()));

        [HttpGet("[action]")]
        public async Task<string> Cars() =>
            await Task.Run(() => JsonConvert.SerializeObject(parking.Cars));

        [HttpGet("[action]")]
        public async Task<string> Log() => await parking.ShowLog();

        [HttpGet("[action]")]
        public async Task<string> Last_Minute_Transactions() =>
            await Task.Run(() => JsonConvert.SerializeObject(parking.Transactions));

        [HttpGet("[action]/{id}")]
        public async Task<string> Cars(int id) =>
            await Task.Run(() => JsonConvert.SerializeObject(parking.Cars.Find(x=>x.Id == id)));

        [HttpPut("[action]/{id}&{balance:int}")]
        public async Task<string> Recharge_Balance(int id, int balance) => await parking.RechargeBalance(id, balance);

        [HttpPost("[action]/{type}&{balance:int}")]
        public async Task<string> Add_Car(int balance, int type) => await parking.AddCar(balance, type);

        [HttpDelete("[action]/{id}")]
        public async Task<string> Remove_Car(int id) => await parking.RemoveCar(id);
    }
}