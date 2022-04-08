using BankSimulator.Models;
using BankSimulator.Repositories;
using BankSimulator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankSimulator.Test
{
    [TestFixture]
    public class Tests
    {
        private readonly IBankService _bankService;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private Account testAccount;

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public async Task ProcessPayment_ValidPayment_ShouldFullyProcess()
        {
            // Arrange
            testAccount = new Account
            {
                AccountNumber = "123",
                CardNumber = "4012888888881881",
                Balance = 1000,
                Currency = "MUR",
                CVV = "123",
                ExpiryMonth = 12,
                ExpiryYear = 2022
            };

            await _accountRepository.CreateAsync(testAccount);

            // Act
            Payment testPayment = new Payment
            {
                Id = 1,
                CardNumber = "4012888888881881",
                Amount = 100,
                Currency = "MUR",
                CVV = "123",
                ExpiryMonth = 12,
                ExpiryYear = 2022
            };
            IActionResult result = await _bankService.ProcessPayment(testPayment);

            Account updatedAccount = await _accountRepository.GetByIdAsync(testAccount.Id);
            if (updatedAccount.Balance != testAccount.Balance - testPayment.Amount)
                Assert.Fail();

            IEnumerable<Transaction> createdTransactions = await _transactionRepository
                .GetAsync(t => t.AccountId == testAccount.Id);
            if (createdTransactions.Any())
            {
                Transaction createdTransaction = createdTransactions.First();

                if (
                    createdTransaction.RequesterId != testPayment.Id ||
                    createdTransaction.Status != "Complete" ||
                    createdTransaction.Amount != testPayment.Amount ||
                    createdTransaction.Currency != testAccount.Currency ||
                    createdTransaction.Type != "Debit"
                )
                    Assert.Fail();
            }
            else
                Assert.Fail();

            Assert.Pass();

            IEnumerable<Transaction> testTransactions = await _transactionRepository
                .GetAsync(t => t.AccountId == testAccount.Id);

            if (testTransactions.Any())
            {
                Transaction testTransaction = testTransactions.First();
                await _transactionRepository.DeleteAsync(testTransaction);
            }

            await _accountRepository.DeleteAsync(testAccount);
        }

        [TearDown]
        public void TearDown()
        {
            
        }
    }
}