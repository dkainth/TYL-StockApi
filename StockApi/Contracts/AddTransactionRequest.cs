using System.ComponentModel.DataAnnotations;

namespace StockApi.Contracts
{
    public class AddTransactionRequest
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(5, MinimumLength = 3)]
        public string Symbol { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public DateTime TransactionDate { get; set; }

        public int BrokerId { get; set; }
    }
}
