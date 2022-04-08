using PaymentNs.Application.Contracts.Persistence;
using PaymentNs.Domain.Entities;
using PaymentNs.Infrastructure.Persistence;

namespace PaymentNs.Infrastructure.Repositories
{
    public class PaymentRepository : RepositoryBase<Payment>, IPaymentRepository
    {
        public PaymentRepository(PaymentContext dbContext) : base(dbContext)
        { }
    }
}