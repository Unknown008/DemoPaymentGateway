using BankSimulator.Models;
using BankSimulator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankSimulator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankController : ControllerBase
    {
        private readonly ILogger<BankController> _logger;
        private readonly IBankService _bankService;

        public BankController(
            ILogger<BankController> logger, 
            IBankService bankService
        )
        {
            _logger = logger;
            _bankService = bankService;
        }

        /// <summary>
        /// Processes payments
        /// </summary>
        /// <param name="payment"></param>
        /// <returns>Response object indicating if payment was successful or not</returns>
        /// <exception cref="Exception">Failed to process payment</exception>
        [HttpPost(Name = "ProcessPayment")]
        public async Task<IActionResult> ProcessPayment([FromBody] Payment payment)
        {
            try
            {
                _logger.LogInformation("BankSimulatorController: Processing payment");
                return await _bankService.ProcessPayment(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BankSimulatorController: Failed to process payment.");
                throw new Exception("BankSimulatorController: Failed to process payment.");
            }
        }
    }
}