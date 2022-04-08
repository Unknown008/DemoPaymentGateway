using PaymentNs.Application.Models;
using PaymentNs.Domain.Entities;

namespace PaymentNs.Application.Contracts.Infrastructure
{
    public interface IForwardPaymentRequestService
    {
        Task<BankResponse> ForwardPaymentRequest(Payment payment);
    }
}
