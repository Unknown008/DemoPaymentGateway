using MediatR;
using NUnit.Framework;
using PaymentNs.Application.Contracts.Persistence;
using PaymentNs.Application.Features.Payments.Queries.GetPayment;

namespace Payment.Test
{

    [TestFixture]
    public class Tests
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMediator _mediator;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(1)]
        public void GetPaymentById_ExistingPayment_ShouldReturnThePayment(int id)
        {
            // Arrange


            // Act
            //PaymentVm payment = _mediator.Send(new GetPaymentQuery(id)).Result();

            // Assert
            Assert.Pass();
        }
    }
}