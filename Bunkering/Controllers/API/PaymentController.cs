using Bunkering.Core.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bunkering.Access;
using Bunkering.Core.ViewModels;
using Bunkering.Access.Services;
using Bunkering.Core.Utils;
using Microsoft.Extensions.Options;

namespace Bunkering.Controllers.API
{
    [Authorize]
    [Route("api/bunkering/[controller]")]
    [ApiController]
    public class PaymentController : ResponseController
    {
        private readonly PaymentService _payment;
        private readonly AppSetting _appSetting;

        public PaymentController(PaymentService payment, IOptions<AppSetting> appSetting)
        {
            _payment = payment;
            _appSetting = appSetting.Value;
        }

        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(typeof(ApiResponse), 409)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        [HttpPost]
        [Route("create-payment")]
        public async Task<IActionResult> CreatePayment(int id) => Response(await _payment.CreatePayment(id).ConfigureAwait(false));
        [HttpGet]
        [Route("pay-online")]
        public IActionResult PayOnline(string rrr) => Redirect($"{_appSetting}/Payment/Pay?rrr={rrr}");

        [HttpPost]
        [Route("remita")]
        public async Task<IActionResult> Remita(int id, RemitaResponse model)
        {
            var payment = await _payment.ConfirmPayment(id);
            return Redirect($"{_appSetting.LoginUrl}/company/paymentsum/{id}");
        }
    }
}
