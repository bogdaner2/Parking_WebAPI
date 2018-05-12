using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Parking.Data;

namespace Parking_WebAPI.Controllers
{
    public class ParkingController : Controller
    {
        private readonly Parking.Data.Parking parking = Parking.Data.Parking.Instance;
        // GET: api/parking/Index
        public ParkingController()
        {

        }

        public string Info()
        {
            return "Select an action and enter its number:\n" +
                   "1)Add car \n" +
                   "2)Remove car \n" +
                   "3)Recharge car balance \n" +
                   "4)Show transaction history for the last minute\n" +
                   "5)Show Parking balance \n" +
                   "6)Show count of free spots \n" +
                   "7)Show Transactions.log\n" +
                   "8)Show cars\n" +
                   "9)Exit";
        }
        [HttpGet("api/parking/[action]",Name = "GetFreeSpots")]
        public string GetFreeSpots()
        {
            return "Free spots "+ parking.Settings.ParkingPlace;
        }

        [HttpGet]
        public string ShowBalance()
        {
            return JsonConvert.SerializeObject(parking.Balance);
        }

        [HttpGet]
        public int AmountOfoccupiedSpots()
        {
            return 0;
        }

        [HttpPost]
        public string AddNewCar()
        {
            return " ";
        }

        [HttpGet]
        public string ShowCars()
        {
            return JsonConvert.SerializeObject(parking.Cars);
        }


        [Route("api/parking/addcar/{id}&{balance}")]
        [HttpPut]
        public ActionResult AddCar(int balance,int id)
        {
            parking.Cars[id].RechargeBalance(balance);
            return NoContent();
        }
    }
}