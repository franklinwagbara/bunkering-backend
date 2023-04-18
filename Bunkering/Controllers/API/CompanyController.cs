using Bunkering.Access.Services;
using Bunkering.Core.Data;
using Bunkering.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bunkering.Controllers.API
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ResponseController
    {
        private readonly CompanyService _companySrevice;

        public CompanyController(
            CompanyService companyService)
        {
            _companySrevice = companyService;
        }

        /// <summary>
        /// This endpoint is used to fetch company's dashboard info
        /// </summary>
        /// <returns>Returns a success message or rotherwise</returns>
        /// <remarks>
        /// 
        /// Sample Request
        /// GET: api/company/dashboard
        /// 
        /// </remarks>
        /// <response code="200">Returns a summary of the company's dashboard </response>
        /// <response code="404">Returns not found </response>
        /// <response code="401">Unauthorized user </response>
        /// <response code="400">Internal server error - bad request </response>
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(typeof(ApiResponse), 405)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        [Route("dashboard")]
        [HttpGet]
        public async Task<IActionResult> Dashboard() => Response(await _companySrevice.Dashboard());

        /// <summary>
        /// This endpoint is used to fetch company's profile 
        /// </summary>
        /// <returns>Returns a success message or otherwise</returns>
        /// <remarks>
        /// 
        /// Sample Request
        /// GET: api/company/dashboard
        /// 
        /// </remarks>
        /// <param name="email">This is the email of the company which maybe be null if the user is loged in</param>
        /// <response code="200">Returns an object with the company's profile </response>
        /// <response code="404">Returns not found </response>
        /// <response code="401">Unauthorized user </response>
        /// <response code="400">Internal server error - bad request </response>
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(typeof(ApiResponse), 405)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        [Route("get-profile")]
        [HttpGet]
        public async Task<IActionResult> GetProfile(string email = null) => Response(await _companySrevice.GetProfile(email));

        /// <summary>
        /// This endpoint is used to fetch company's dashboard info
        /// </summary>
        /// <returns>Returns a success message or rotherwise</returns>
        /// <remarks>
        /// 
        /// Sample Request
        /// GET: api/company/dashboard
        /// 
        /// </remarks>
        /// <param name="model">This is the model of the company's profile update</param>
        /// <param name="oldemail">This is the old emailof the company's profile</param>
        /// <response code="200">Returns a summary of the company's dashboard </response>
        /// <response code="404">Returns not found </response>
        /// <response code="401">Unauthorized user </response>
        /// <response code="400">Internal server error - bad request </response>
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(typeof(ApiResponse), 405)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        [Route("update-profile")]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(CompanyInformation model, string oldemail) => Response(await _companySrevice.UpdateProfile(model, oldemail));

    }
}
