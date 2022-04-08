using MediatR;

namespace PaymentNs.Application.Features.Payments.Queries.GetPayment
{
    public class GetPaymentQuery : IRequest<PaymentVm>
    {
        public int Id { get; set; }

        public GetPaymentQuery(int? id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
    }
}
