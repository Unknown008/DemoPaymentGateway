using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentNs.Application.Exceptions;
using PaymentNs.Application.Features.Payments.Commands.ForwardPayment;
using PaymentNs.Application.Features.Payments.Queries.GetPayment;
using PaymentNs.Application.Models;
using System.Net;

namespace PaymentNs.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Retrieves an existing payment made previously using its ID
    /// </summary>
    /// <param name="id">The payment ID</param>
    /// <returns>The payment details with the card number masked</returns>
    [HttpGet("{id}", Name = "GetPayment")]
    [ProducesResponseType(typeof(PaymentVm), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaymentVm>> GetPaymentById(int id)
    {
        GetPaymentQuery query = new GetPaymentQuery(id);
        try
        {
            PaymentVm payment = await _mediator.Send(query);
            return Ok(payment);
        }
        catch (NotFoundException ex)
        {
            return new NotFoundObjectResult(new ExceptionMessage()
            {
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Records a new payment request after validating the payment details
    /// and forwards it to the bank simulator for processing
    /// </summary>
    /// <param name="command">Payment object containing the payment details</param>
    /// <returns>
    /// A response object indicating whether the payment was successful or not,
    /// and a reason if it was unsuccessful
    /// </returns>
    [HttpPost(Name = "ForwardPayment")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BankResponse>> ForwardPayment([FromBody] ForwardPaymentCommand command)
    {
        try
        {
            return await _mediator.Send(command);
        }
        catch(ValidationException ex)
        {
            return new BadRequestObjectResult(new ExceptionMessage()
            {
                Message = ex.Message
            });
        }
    }
}
