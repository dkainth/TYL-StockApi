namespace StockApi.Repository.Models
{
    public class Broker
    {
        public int Id { get; init; }

        public string Name { get; init; }


        public Broker(string name)
        {
            Name = name;
        }
    }
}
