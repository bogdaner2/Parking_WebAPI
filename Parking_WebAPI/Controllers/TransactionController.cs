using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Parking.Data;

namespace Parking_WebAPI.Controllers
{
    [Route("api/[controller]/")]
    public class TransactionController : Controller
    {
        private readonly Parking.Data.Parking parking = Parking.Data.Parking.Instance;

        public async Task<string> Log() => await parking.ShowLog();

        [HttpGet("[action]")]
        public async Task<string> Last_Minute_Transactions() =>
            await Task.FromResult(JsonConvert.SerializeObject(parking.Transactions));

        [HttpGet("[action]/{id}")]
        public async Task<string> Last_Minute_Transactions(int id) =>
            await Task.FromResult(JsonConvert.SerializeObject(Transaction.TransactionsForCurtainCar(id, parking)));

        [HttpPut("[action]/{id}")]
        public async Task<string> Recharge_Balance(int id, [FromBody]int balance) => await parking.RechargeBalance(id, balance);
    }
}