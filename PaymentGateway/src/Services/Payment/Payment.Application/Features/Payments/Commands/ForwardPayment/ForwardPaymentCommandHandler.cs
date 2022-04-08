using AutoMapper;
using MediatR;
using PaymentNs.Application.Contracts.Infrastructure;
using PaymentNs.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using PaymentNs.Domain.Entities;
using PaymentNs.Application.Models;

namespace PaymentNs.Application.Features.Payments.Commands.ForwardPayment
{
    public class ForwardPaymentCommandHandler : IRequestHandler<ForwardPaymentCommand, BankResponse>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IForwardPaymentRequestService _forwardPaymentRequestService;
        private readonly ILogger<ForwardPaymentCommandHandler> _logger;

        public ForwardPaymentCommandHandler(
            IPaymentRepository paymentRepository, 
            IMapper mapper, 
            IForwardPaymentRequestService forwardPaymentRequestService,
            ILogger<ForwardPaymentCommandHandler> logger
        ) {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _forwardPaymentRequestService = forwardPaymentRequestService ?? throw new ArgumentNullException(nameof(forwardPaymentRequestService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BankResponse> Handle(ForwardPaymentCommand request, CancellationToken cancellationToken)
        {
            Payment paymentToForward = _mapper.Map<Payment>(request);
            
            // Set the payment to pending for now and insert it into the DB
            paymentToForward.PaymentStatus = "Pending";
            Payment newPayment = await _paymentRepository.CreateAsync(paymentToForward);

            _logger.LogInformation($"New payment {newPayment.Id} created.");

            BankResponse response = await _forwardPaymentRequestService.ForwardPaymentRequest(newPayment);

            // Update payment status following bank response
            newPayment.PaymentStatus = response.Status;

            await _paymentRepository.UpdateAsync(newPayment);

            return new BankResponse
            {
                Status = newPayment.PaymentStatus,
                Message = newPayment.PaymentStatus != "Successful" ? response.Message : null,
            };
        }
    }
}
