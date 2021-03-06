namespace StockApi.Repository.Models
{
    public class Transaction
    {
        public long Id { get; init; }

        public decimal PriceGbp { get; init; }

        public decimal Quantity { get; init; }

        public DateTime TransactionDate { get; init; }

        public Broker Broker { get; init; }

        public Stock Stock { get; init; }


        private Transaction()
        {
        }

        public Transaction(Stock stock, decimal priceGbp, decimal quantity, DateTime transactionDate, Broker broker) : this()
        {
            Stock = stock;
            PriceGbp = priceGbp;
            Quantity = quantity;
            TransactionDate = transactionDate;
            Broker = broker;
        }

    }
}
