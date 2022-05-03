using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using StockApi.Contracts;
using StockApi.Controllers;
using StockApi.Repository.Models;
using StockApi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApi.Tests.Controllers
{
    public class TransactionControllerTests
    {
        private IBrokerRepository _brokerRepository;
        private IStockRepository _stockRepository;
        private ITransactionRepository _transactionRepository;

        private TransactionController _controller;

        [SetUp]
        public void SetUp()
        {
            _brokerRepository = Substitute.For<IBrokerRepository>();
            _stockRepository = Substitute.For<IStockRepository>();
            _transactionRepository = Substitute.For<ITransactionRepository>();

            _controller = new TransactionController(_brokerRepository, _stockRepository, _transactionRepository);
        }


        [Test]
        public async Task Add_ShouldReturnBadResult_WhenRequestNull()
        {

            var result = await _controller.Add(null);

            await _brokerRepository.DidNotReceiveWithAnyArgs().GetById(Arg.Any<int>());
            _stockRepository.DidNotReceiveWithAnyArgs().GetBySymbol(Arg.Any<string>());
            await _transactionRepository.DidNotReceiveWithAnyArgs().Add(Arg.Any<Transaction>());

            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());

        }


        [Test]
        public async Task Add_ShouldReturnBadResult_WhenBrokerDoesNotExisit()
        {
            var request = new AddTransactionRequest
            {
                Symbol = "ABC",
                Price = 123,
                Quantity = 1,
                BrokerId = 100
            };

            _brokerRepository.GetById(default).ReturnsForAnyArgs((Broker?)null);
            _stockRepository.GetBySymbol(Arg.Any<string>()).ReturnsForAnyArgs(new Stock("ABC", "Test Stock"));

            var result = await _controller.Add(request);



            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());

            await _brokerRepository.Received(1).GetById(request.BrokerId);

            _stockRepository.DidNotReceiveWithAnyArgs().GetBySymbol(Arg.Any<string>());

            await _transactionRepository.DidNotReceiveWithAnyArgs().Add(Arg.Any<Transaction>());
        }


        [Test]
        public async Task Add_ShouldReturnBadResult_WhenStockDoesNotExisit()
        {
            var request = new AddTransactionRequest
            {
                Symbol = "ABC",
                Price = 123,
                Quantity = 1,
                BrokerId = 100
            };

            _brokerRepository.GetById(default).ReturnsForAnyArgs(new Broker("Bill"));
            _stockRepository.GetBySymbol(Arg.Any<string>()).ReturnsForAnyArgs((Stock?)null);

            var result = await _controller.Add(request);



            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());

            await _brokerRepository.Received(1).GetById(request.BrokerId);

            _stockRepository.Received(1).GetBySymbol(request.Symbol);

            await _transactionRepository.DidNotReceiveWithAnyArgs().Add(Arg.Any<Transaction>());
        }


        [Test]
        public async Task Add_ShouldReturnOk_WhenRequestIsValid()
        {

            var request = new AddTransactionRequest
            {
                Symbol = "AAPL",
                Price = 1222,
                Quantity = 12,
                TransactionDate = DateTime.Now,
                BrokerId = 1
            };

            _brokerRepository.GetById(request.BrokerId).Returns(new Broker("Bill"));
            _stockRepository.GetBySymbol(request.Symbol).ReturnsForAnyArgs(new Stock("AAPL", "Test Stock"));

            var result = await _controller.Add(request);


            Assert.AreEqual(typeof(OkResult), result.GetType());

            await _brokerRepository.Received(1).GetById(request.BrokerId);

            _stockRepository.Received(1).GetBySymbol(request.Symbol);

            await _transactionRepository.Received(1).Add(Arg.Is<Transaction>(st => st.Stock.Symbol == "AAPL"
                                                                                    && st.PriceGbp == request.Price
                                                                                    && st.Quantity == request.Quantity
                                                                                    && st.TransactionDate == request.TransactionDate
                                                                                    && st.Broker.Name == "Bill"));
        }
    }
}
