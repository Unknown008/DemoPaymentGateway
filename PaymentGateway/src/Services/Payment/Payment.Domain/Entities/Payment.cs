using PaymentNs.Domain.Common;

namespace PaymentNs.Domain.Entities
{
    public class Payment : EntityBase
    {
        public new int Id { get; protected set; }
        public string? CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int Amount { get; set; }
        public string? Currency { get; set; }
        public string? CVV { get; set; }
        public string? PaymentStatus { get; set; }
    }
}
