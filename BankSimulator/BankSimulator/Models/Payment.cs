namespace BankSimulator.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string? CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int Amount { get; set; }
        public string? Currency { get; set; }
        public string? CVV { get; set; }
        public string? PaymentStatus { get; set; }
    }
}
