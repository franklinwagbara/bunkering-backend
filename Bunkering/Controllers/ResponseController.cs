using Bunkering.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using System.Net;

namespace Bunkering.Controllers
{
    public class ResponseController : ControllerBase
    {
        protected new IActionResult Response(ApiResponse response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
                return Ok(new
                {
                    success = true,
                    data = response
                });

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(new
                {
                    success = false,
                    data = response
                });

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(new
                {
                    success = false,
                    data = response
                });

            if (response.StatusCode == HttpStatusCode.Conflict)
                return Conflict(new
                {
                    success = false,
                    data = response
                });

            return NotFound(new
            {
                success = false,
                data = response
            });
        }
    }
}
