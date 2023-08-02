﻿using Bunkering.Access.Services;
using Bunkering.Core.Utils;
using Bunkering.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Runtime.CompilerServices;

namespace Bunkering.Controllers.API
{
	[AllowAnonymous]
	[ApiController]
	[Route("api/bunkering/[controller]")]

	public class LibraryController : ResponseController
	{
		private readonly LibraryService libraryService;
		private readonly AppStageDocService _appStageDocService;


		public LibraryController(LibraryService locationService_, AppStageDocService appStageDocService)
		{
			this.libraryService = locationService_;

			_appStageDocService = appStageDocService;

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
		[Route("States")]
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
		[Route("Lga")]
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
		[Route("LgaByStateId")]
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

		/// <summary>
		/// This endpoint is used to get all AppStatus
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
		[Route("GetAppStatus")]
		[HttpGet]

		public async Task<IActionResult> GetAppStatus() => Ok(EnumExtension.GetNames<AppStatus>());



		/// <summary>
		/// This endpoint is used to get all App Actions
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
		[Route("GetAllAppActions")]
		[HttpGet]

		public async Task<IActionResult> getAppActions() => Ok(EnumExtension.GetNames<AppActions>());



		/// <summary>
		/// This endpoint is used to get Application Types
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
		[Route("ApplicationTypes")]
		[HttpGet]

		public async Task<IActionResult> ApplicationType() => Response(await libraryService.ApplicationType());


		/// <summary>
		/// This endpoint is used to get Facility Types
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
		[Route("FacilityTypes")]
		[HttpGet]

		public async Task<IActionResult> FacilityTypes() => Response(await libraryService.FacilityTypes());


		/// <summary>
		/// This endpoint is used to get All Roles
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
		[Route("Roles")]
		[HttpGet]

		public async Task<IActionResult> GetRoles() => Response(await libraryService.GetRoles());


		/// <summary>
		/// This endpoint is used to get All FacilityType Documents
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
		[Route("GetAll-Facility-Type-Doc")]
		[HttpGet]
		public async Task<IActionResult> GetAllFADDoc() => Response(await _appStageDocService.GetAllFADDoc());


		/// <summary>
		/// This endpoint is used to create FacilityType Documents
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
		[Route("Create-FacilityType-Doc")]
		[HttpPost]

		public async Task<IActionResult> createFADDoc([FromBody] AppStageDocsViewModel model) => Response(await _appStageDocService.CreateFADDoc(model));





		/// <summary>
		/// This endpoint is used to edit FacilityType Documents
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
		[Route("Edit-FacilityType-Doc")]
		[HttpPut]

		public async Task<IActionResult> UpdateFADDoc(int id) => Response(await _appStageDocService.UpdateFADDoc(id));


		/// <summary>
		/// This endpoint is used to delete FacilityType Documents
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
		[Route("Delete-FacilityType-Doc")]
		[HttpDelete]

		public async Task<IActionResult> DeleteFADDoc(int id) => Response(await _appStageDocService.DeleteFADDoc(id));
	}
}
