using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Parking_WebAPI.Controllers
{
    public class ParkingController : Controller
    {
        private readonly Parking.Data.Parking parking = Parking.Data.Parking.Instance;
        // GET: api/parking/Index
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
        public string GetFreeSpots()
        {
            return "Free spots "+ parking.Settings.ParkingPlace;
        }


    }
}