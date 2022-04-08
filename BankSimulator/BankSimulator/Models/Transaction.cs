using System.ComponentModel.DataAnnotations.Schema;

namespace BankSimulator.Models
{
    public class Transaction : EntityBase
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int RequesterId { get; set; }
        public string? Status { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string? Type { get; set; }

        [ForeignKey("AccountId")]
        public Account? Account { get; set; }
    }
}
