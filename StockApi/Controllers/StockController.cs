using Microsoft.AspNetCore.Mvc;
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

            var stockPrices = _transactionRepository.FindLastPriceForStocks(stock);

            var response = stockPrices.Select(sp => new StockPrice { Symbol = sp.Key, Price = sp.Value }).SingleOrDefault();

            return Ok(response);

        }

        [HttpPost]
        [Route("stock-prices")]
        public async Task<IActionResult> GetPrices(string[] symbols)
        {
            //validate request
            if (symbols == null || symbols.Count() == 0)
                return BadRequest();

            var stocks =new List<Stock>();

            foreach(var s in symbols)
                if (!TryAndGetStock(s, out var stock))
                    return BadRequest("Invalid symbol");
                else
                    stocks.Add(stock);


            var stockPrices = _transactionRepository.FindLastPriceForStocks(stocks.ToArray());

            var response = stockPrices.Select(sp => new StockPrice { Symbol = sp.Key, Price = sp.Value });

            return Ok(response);
        }

        [HttpGet]
        [Route("stock-prices-all")]
        public async Task<IActionResult> GetPrices()
        {

            //TODO : Add method to the transaction repo to get the latest prices for all stocks

            return Ok();

        }


        private bool TryAndGetStock(string symbol, out Stock? stock)
        {
            stock = null;

            //TODO : enhance the check to see if it is between 3 and 5 chars and move this to a static method inside the Stock model
            if (string.IsNullOrWhiteSpace(symbol))
                return false;

            stock = _stockRepository.GetBySymbol(symbol);

            return stock != null;
        }
    }
}