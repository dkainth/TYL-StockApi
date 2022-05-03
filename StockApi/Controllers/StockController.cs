using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace StockApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {

        public StockController()
        {
        }

        [HttpGet]
        [Route("stock-price")]
        public async Task<IActionResult> GetPrice(string symbol)
        {
            //validate request

            //add to the repo

            return Ok();
        }

        [HttpGet]
        [Route("stock-prices")]
        public async Task<IActionResult> GetPrices(string[] symbol)
        {
            //validate request

            //add to the repo

            return Ok();
        }

        [HttpGet]
        [Route("stock-prices-all")]
        public async Task<IActionResult> GetPrices()
        {
            //validate request

            //add to the repo

            return Ok();
        }
    }
}