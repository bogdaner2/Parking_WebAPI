using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Parking_WebAPI.Controllers
{
    [Route("api/[controller]/")]
    public class CarController : Controller
    {
        private readonly Parking.Data.Parking parking = Parking.Data.Parking.Instance;

        [HttpGet("[action]/{id}")]
        public async Task<string> Cars(int id) =>
            await Task.FromResult(JsonConvert.SerializeObject(parking.Cars.Find(x => x.Id == id)));

        public async Task<string> Cars() =>
            await Task.FromResult(JsonConvert.SerializeObject(parking.Cars));

        [HttpPost("[action]/{type}")]
        public async Task<string> Add_Car(int type, [FromBody] int balance) => await parking.AddCar(balance, type);

        [HttpDelete("[action]/{id}")]
        public async Task<string> Remove_Car(int id) => await parking.RemoveCar(id);
    }
}