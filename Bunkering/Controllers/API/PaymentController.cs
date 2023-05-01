using Bunkering.Core.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bunkering.Access;
using Bunkering.Core.ViewModels;
using Bunkering.Access.Services;
using Bunkering.Core.Utils;

namespace Bunkering.Controllers.API
{
    [Authorize]
    [Route("api/bunkering/[controller]")]
    [ApiController]
    public class PaymentController : ResponseController
    {
        private readonly AppConfiguration _appConfig;
        private readonly PaymentService _payment;

        public PaymentController(AppConfiguration appConfig, PaymentService payment)
        {
            _appConfig = appConfig;
            _payment = payment;
        }

        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(typeof(ApiResponse), 409)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        [HttpPost]
        [Route("create-payment")]
        public async Task<IActionResult> CreatePayment(string id) => Response(await _payment.CreatePayment(id).ConfigureAwait(false));
        [HttpGet]
        [Route("pay-online")]
        public IActionResult PayOnline(string rrr) => Redirect($"{_appConfig.Config().GetValue("elpsurl")}/Payment/Pay?rrr={rrr}");

        [HttpPost]
        [Route("remita")]
        public async Task<IActionResult> Remita(string id, RemitaResponse model)
        {
            var payment = await _payment.ConfirmPayment(id);
            return Redirect($"{_appConfig.Config().GetValue("loginurl")}/company/paymentsum/{id}");
        }
    }
}
