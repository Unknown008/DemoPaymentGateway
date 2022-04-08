using System.Text.RegularExpressions;

namespace PaymentNs.Application.Features.Payments.Queries.GetPayment
{
    public class PaymentVm
    {
        public int Id { get; protected set; }
        public string? CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int Amount { get; set; }
        public string? Currency { get; set; }
        public string? CVV { get; set; }
        public string? PaymentStatus { get; set; }
        public int? BankTransactionId { get; set; }

        public PaymentVm(
            int Id,
            string? CardNumber,
            int ExpiryMonth,
            int ExpiryYear,
            int Amount,
            string? Currency,
            string? CVV,
            string? PaymentStatus,
            int? BankTransactionId
        )
        {
            this.Id = Id;
            this.CardNumber = Regex.Replace(CardNumber, @"^([0-9]+)([0-9]{4})$", delegate (Match match)
            {
                return new string('X', match.Groups[1].Value.Length) + match.Groups[2].Value;
            });
            this.ExpiryMonth = ExpiryMonth;
            this.ExpiryYear = ExpiryYear;
            this.Amount = Amount;
            this.Currency = Currency;
            this.CVV = CVV;
            this.PaymentStatus = PaymentStatus;
            this.BankTransactionId = BankTransactionId;
        }
    }
}
