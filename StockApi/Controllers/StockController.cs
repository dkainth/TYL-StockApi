using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StockApi.Contracts;
using StockApi.Repository.Models;
using StockApi.Repository.Repositories;

namespace StockApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {

        private readonly IStockRepository _stockRepository;
        private readonly ITransactionRepository _transactionRepository;

        public StockController(IStockRepository stockRepository, ITransactionRepository transactionRepository)
        {
            _stockRepository = stockRepository;
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        [Route("stock-price")]
        public IActionResult GetPrice(string symbol)
        {
            //validate request
            if(!TryAndGetStock(symbol, out var stock))
                return BadRequest("Invalid symbol");

            var prices = _transactionRepository.FindLastPriceByStock(new Stock[] { stock });

            var response = new StockPrice
            {
                Symbol = symbol,
                Price = prices.ContainsKey(stock.Symbol) ? prices[stock.Symbol] : 0
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("stock-prices")]
        public async Task<IActionResult> GetPrices(string[] symbol)
        {
            //validate request

            return Ok();
        }

        [HttpGet]
        [Route("stock-prices-all")]
        public async Task<IActionResult> GetPrices()
        {
            //validate request


            return Ok();
        }

        private bool TryAndGetStock(string symbol, out Stock? stock)
        {
            stock = null;

            if (string.IsNullOrWhiteSpace(symbol))
                return false;

            stock = _stockRepository.GetBySymbol(symbol);

            return stock != null;
        }
    }
}