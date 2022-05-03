namespace StockApi.Repository.Models
{
    public class Stock
    {
        public int Id { get; init; }

        public string Symbol { get; init; }

        public string Name { get; init; }


        private Stock()
        {
        }

        public Stock(string symbol, string name) : this()
        {
            Symbol = symbol;
            Name= name;
        }
    }
}
