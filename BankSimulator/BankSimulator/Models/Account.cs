namespace BankSimulator.Models
{
    public class Account : EntityBase
    {
        public int Id { get; set; }
        public string? AccountNumber { get; set; }
        public string? CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public decimal Balance { get; set; }
        public string? Currency { get; set; }
        public string? CVV { get; set; }
    }
}