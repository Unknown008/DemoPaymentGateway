using AutoMapper;
using PaymentNs.Application.Features.Payments.Commands.ForwardPayment;
using PaymentNs.Application.Features.Payments.Queries.GetPayment;
using PaymentNs.Domain.Entities;

namespace PaymentNs.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Payment, PaymentVm>().ReverseMap();
            CreateMap<Payment, ForwardPaymentCommand>().ReverseMap();
        }
    }
}
