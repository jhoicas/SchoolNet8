using Microsoft.AspNetCore.Mvc;
using School.Contracts.Interfaces;
using School.Contracts.Requests;
using School.Service.Implementations;

namespace School.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {

        private readonly IPaymentService paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            this.paymentService = paymentService;
            this._logger = logger;
        }

        [HttpPost("register-payment/{idEnrollment}")]
        public async Task<IActionResult> RegisterPayment(int idEnrollment)
        {
            try
            {
                var response = await paymentService.Payment(idEnrollment);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering Payment");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
