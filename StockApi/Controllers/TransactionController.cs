using Microsoft.AspNetCore.Mvc;

namespace StockApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {

        public TransactionController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            //validate request

            //add to the repo

            return Ok();
        }
    }
}