using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentNs.Application.Contracts.Persistence;
using PaymentNs.Infrastructure.Repositories;
using PaymentNs.Infrastructure.Persistence;
using PaymentNs.Application.Contracts.Infrastructure;
using PaymentNs.Infrastructure.ForwardPaymentRequest;
using PaymentNs.Application.Models;

namespace PaymentNs.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PaymentContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PaymentConnectionString"))
            );

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            services.AddTransient<IForwardPaymentRequestService, ForwardPaymentRequestService>();

            return services;
        }
    }
}