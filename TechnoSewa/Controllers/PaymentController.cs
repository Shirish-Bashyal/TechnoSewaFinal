using System.Security.Claims;
using Application.DTO.Payment;
using Application.Interfaces.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TechnoSewa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentServics _paymentServics;

        public PaymentController(IPaymentServics paymentServics)
        {
            _paymentServics = paymentServics;
        }

        [HttpGet]
        [Authorize]
        [Route("commission")]
        public async Task<IActionResult> GetCommissionAmount()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await _paymentServics.GetCommissionDetail(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("add")]
        public async Task<IActionResult> AddPayment(TransactionDTO Model)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await _paymentServics.AddPayment(Model, userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }
    }
}
