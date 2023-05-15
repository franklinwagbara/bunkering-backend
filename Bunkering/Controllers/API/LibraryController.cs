using Bunkering.Access.Services;
using Bunkering.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bunkering.Controllers.API
{

    [ApiController]
    [Route("api/bunkering/[controller]")]

    public class LibraryController : ResponseController
    {
        private readonly LibraryService libraryService;


        public LibraryController(LibraryService locationService_)
        {
            this.libraryService = locationService_;

        }


        /// <summary>
        /// This endpoint is used to get all states
        /// </summary>
        /// <returns>Returns a success message</returns>
        /// <remarks>
        /// 
        /// Sample Request
        /// GET: api/location/states
        /// 
        /// </remarks>
        /// <response code="200">Returns a success message </response>
        /// <response code="404">Returns not found </response>
        /// <response code="401">Unauthorized user </response>
        /// <response code="400">Internal server error - bad request </response>
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(typeof(ApiResponse), 405)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        [Produces("application/json")]
        [Route("states")]
        [HttpGet]
        public async Task<IActionResult> GetStates() => Response(await libraryService.GetAllStates());

        //public async Task<IActionResult> GetStates()
        //{
        //    var states = await locationService.GetAllStates();
        //   return Response(states);
        //}

        /// <summary>
        /// This endpoint is used to get all Local Governments
        /// </summary>
        /// <returns>Returns a success message</returns>
        /// <remarks>
        /// 
        /// Sample Request
        /// GET: api/location/local Government 
        /// 
        /// </remarks>
        /// <response code="200">Returns a success message </response>
        /// <response code="404">Returns not found </response>
        /// <response code="401">Unauthorized user </response>
        /// <response code="400">Internal server error - bad request </response>
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(typeof(ApiResponse), 405)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        [Produces("application/json")]
        [Route("LGA")]
        [HttpGet]
        public async Task<IActionResult> GetAllLocalGov() => Response(await libraryService.GetLocalGov());



        /// <summary>
        /// This endpoint is used to get all Local Government by StateId
        /// </summary>
        /// <returns>Returns a success message</returns>
        /// <remarks>
        /// 
        /// Sample Request
        /// GET: api/location/local Government 
        /// 
        /// </remarks>
        /// <response code="200">Returns a success message </response>
        /// <response code="404">Returns not found </response>
        /// <response code="401">Unauthorized user </response>
        /// <response code="400">Internal server error - bad request </response>
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(typeof(ApiResponse), 405)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        [Produces("application/json")]
        [Route("LGAbystateId")]
        [HttpGet]
        public async Task<IActionResult> LGAByStateId(int stateId) => Response(await libraryService.LGA_StateID(stateId));



        /// <summary>
        /// This endpoint is used to get all states by CountryId
        /// </summary>
        /// <returns>Returns a success message</returns>
        /// <remarks>
        /// 
        /// Sample Request
        /// GET: api/location/local Government 
        /// 
        /// </remarks>
        /// <response code="200">Returns a success message </response>
        /// <response code="404">Returns not found </response>
        /// <response code="401">Unauthorized user </response>
        /// <response code="400">Internal server error - bad request </response>
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(typeof(ApiResponse), 405)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        [Produces("application/json")]
        [Route("StatebyCountryId/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> StateByCountID(int id) => Response(await libraryService.State_CountryID(id));



        /// <summary>
        /// This endpoint is used to get all Countries
        /// </summary>
        /// <returns>Returns a success message</returns>
        /// <remarks>
        /// 
        /// Sample Request
        /// GET: api/location/local Government 
        /// 
        /// </remarks>
        /// <response code="200">Returns a success message </response>
        /// <response code="404">Returns not found </response>
        /// <response code="401">Unauthorized user </response>
        /// <response code="400">Internal server error - bad request </response>
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(typeof(ApiResponse), 405)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        [Produces("application/json")]
        [Route("Countries")]
        [HttpGet]
        public async Task<IActionResult> all_countires() => Response(await libraryService.GetCountries());



    }
}
