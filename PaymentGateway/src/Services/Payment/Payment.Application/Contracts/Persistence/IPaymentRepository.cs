using PaymentNs.Domain.Entities;

namespace PaymentNs.Application.Contracts.Persistence
{
    public interface IPaymentRepository : IAsyncRepository<Payment>
    { }
}
