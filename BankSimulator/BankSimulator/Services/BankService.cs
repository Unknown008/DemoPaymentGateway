using BankSimulator.Models;
using BankSimulator.Repositories;
using BankSimulator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankSimulator.Services
{
    public class BankService : IBankService
    {
        private readonly ILogger<BankService> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public BankService(
            ILogger<BankService> logger,
            IAccountRepository accountRepository, 
            ITransactionRepository transactionRepository
        )
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// Processes payments
        /// </summary>
        /// <param name="payment"></param>
        /// <returns>Response object indicating if payment was successful or not</returns>
        /// <exception cref="Exception">Failed to process payment</exception>
        public async Task<IActionResult> ProcessPayment(Payment payment)
        {
            try
            {
                _logger.LogInformation("BankSimulatorService: Processing payment");

                IEnumerable<Account> accounts = await _accountRepository
                    .GetAsync(a => a.CardNumber == payment.CardNumber);

                if (accounts == null || !accounts.Any())
                    return new NotFoundObjectResult(new Response
                    {
                        Status = "Error",
                        Message = "Invalid card"
                    });

                Account account = accounts.First();

                Transaction transaction = new Transaction()
                {
                    AccountId = account.Id,
                    RequesterId = payment.Id,
                    Status = "Pending",
                    Amount = payment.Amount,
                    Currency = payment.Currency,
                    Type = "Debit"
                };
                await _transactionRepository.CreateAsync(transaction);

                // Bunch of checks; giving rather vague reason to discourage malicious intent (e.g if the perpetrator knows cvv is wrong,
                // then they could keep trying to change the cvv)
                if (
                    account.CVV != payment.CVV ||
                    account.ExpiryMonth != payment.ExpiryMonth ||
                    account.ExpiryYear != payment.ExpiryYear ||
                    DateTime.ParseExact(string.Format("{0}-{1}-01", account.ExpiryYear, account.ExpiryMonth), "yyyy-M-dd", null) <=
                        DateTime.Now.AddMonths(-1)
                )
                {
                    transaction.Status = "Declined";
                    await _transactionRepository.UpdateAsync(transaction);
                    return new BadRequestObjectResult(new Response
                    {
                        Status = "Error",
                        Message = "Invalid card"
                    });
                }

                // This validation can be improved by implementing exchange rates
                if (account.Currency != payment.Currency)
                {
                    transaction.Status = "Declined";
                    await _transactionRepository.UpdateAsync(transaction);
                    return new BadRequestObjectResult(new Response
                    {
                        Status = "Error",
                        Message = "Currency mismatch"
                    });
                }

                // If exchange rates implemented, then like-currency amounts should be compared
                if (account.Balance < payment.Amount)
                {
                    transaction.Status = "Declined";
                    await _transactionRepository.UpdateAsync(transaction);
                    return new BadRequestObjectResult(new Response
                    {
                        Status = "Error",
                        Message = "Insufficient funds"
                    });
                }

                transaction.Status = "Complete";
                await _transactionRepository.UpdateAsync(transaction);

                account.Balance -= payment.Amount;
                await _accountRepository.UpdateAsync(account);

                return new OkObjectResult(new Response
                {
                    Status = "Complete",
                    Message = transaction.RequesterId.ToString()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BankSimulatorService: Failed to process payment.");
                throw new Exception("BankSimulatorService: Failed to process payment.");
            }
        }
    }
}
