using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PaymentNs.Application.Contracts.Infrastructure;
using PaymentNs.Application.Models;
using PaymentNs.Domain.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace PaymentNs.Infrastructure.ForwardPaymentRequest
{
    public class ForwardPaymentRequestService : IForwardPaymentRequestService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ForwardPaymentRequestService> _logger;

        public ForwardPaymentRequestService(IConfiguration configuration, ILogger<ForwardPaymentRequestService> logger)
        {
            _configuration = configuration;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Makes an API call to the bank to process the payment
        /// </summary>
        /// <param name="payment">Payment object containing the details of the payment</param>
        /// <returns>The bank response</returns>
        public async Task<BankResponse> ForwardPaymentRequest(Payment payment)
        {
            _logger.LogInformation("Forwarding payment request");
            HttpClient client = new HttpClient();

            string url = _configuration.GetSection("BankSettings").GetSection("Url").Value;

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            BankResponse bankResponse = new BankResponse();
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(
                "bank", payment);
                string bankResponseStr = await response.Content.ReadAsStringAsync();
                bankResponse = JsonConvert.DeserializeObject<BankResponse>(bankResponseStr);
                
                response.EnsureSuccessStatusCode();
                bankResponse.Status = "Successful";
            }
            catch
            {
                bankResponse.Status = "Unsuccessful";
            }

            return bankResponse;
        }
    }
}