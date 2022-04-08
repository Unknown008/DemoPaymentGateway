using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PaymentNs.Application.Contracts.Persistence;
using PaymentNs.Application.Exceptions;
using PaymentNs.Domain.Entities;

namespace PaymentNs.Application.Features.Payments.Queries.GetPayment
{
    public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, PaymentVm>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetPaymentQueryHandler(IPaymentRepository paymentRepository, IMapper mapper, ILogger<GetPaymentQueryHandler> logger)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger;

        }

        public async Task<PaymentVm> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            Payment payment = await _paymentRepository.GetByIdAsync(request.Id);
            if (payment == null)
            {
                _logger.LogError("Payment does not exist");
                throw new NotFoundException(nameof(Payment), request.Id);
            }
            return _mapper.Map<PaymentVm>(payment);
        }
    }
}
