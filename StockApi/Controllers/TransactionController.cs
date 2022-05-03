using Microsoft.AspNetCore.Mvc;
using StockApi.Contracts;
using StockApi.Repository.Models;
using StockApi.Repository.Repositories;

namespace StockApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IBrokerRepository _brokerRepository;
        private readonly IStockRepository _stockRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(IBrokerRepository brokerRepository, IStockRepository stockRepository, ITransactionRepository transactionRepository)
        {
            _brokerRepository = brokerRepository;
            _stockRepository = stockRepository;
            _transactionRepository = transactionRepository;
        }

        //TODO : Validate the request check username and password / token before allowing a transacton to be added
        //possible the request should be signed too
        //use ActionFilter so we can choose which endpoints need extra validation / checking
        [HttpPost]
        public async Task<IActionResult> Add(AddTransactionRequest request)
        {
            //validate request
            if (request == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var broker = await _brokerRepository.GetById(request.BrokerId);

            if (broker == null)
                return BadRequest("Invalid Broker");

            var stock = _stockRepository.GetBySymbol(request.Symbol);

            if (stock == null)
                return BadRequest("Invalid Stock");

            //add to the repo
            await _transactionRepository.Add(new Transaction(stock, request.Price, request.Quantity, request.TransactionDate, broker));

            await _transactionRepository.SaveChanges();

            return Ok();

        }
    }
}