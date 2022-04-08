using BankSimulator.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankSimulator.Services.Interfaces
{
    public interface IBankService
    {
        Task<IActionResult> ProcessPayment(Payment payment);
    }
}
