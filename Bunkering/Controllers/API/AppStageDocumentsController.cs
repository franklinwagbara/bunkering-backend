﻿using Bunkering.Access.Services;
using Bunkering.Core.Data;
using Bunkering.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bunkering.Controllers.API
{
    public class AppStageDocumentsController : ResponseController
    {
        private readonly AppStageDocService _appDocService;

        public AppStageDocumentsController(AppStageDocService appDocService)
        {
            _appDocService = appDocService;
        }

        /// <summary>
        /// This endpoint is used to approve or reject an application
        /// </summary>
        /// <returns>Returns a success message or rotherwise</returns>
        /// <remarks>
        /// 
        /// Sample Request
        /// GET: api/appstagedoc/getall
        /// 
        /// </remarks>
        /// <response code="200">Returns an application info </response>
        /// <response code="404">Returns not found </response>
        /// <response code="401">Unauthorized user </response>
        /// <response code="400">Internal server error - bad request </response>
        [Route("getall")]
        [HttpGet]
        public async Task<IActionResult> Index() => Response(await _appDocService.GetAll());

        /// <summary>
        /// This endpoint is used to add document to facility stage
        /// </summary>
        /// <returns>Returns a success message or rotherwise</returns>
        /// <remarks>
        /// 
        /// Sample Request
        /// POST: api/application/add-document
        /// 
        /// </remarks>
        /// <param name="model">This is the message attaced to the processing of the application </param>
        /// <response code="200">Returns an application info </response>
        /// <response code="404">Returns not found </response>
        /// <response code="401">Unauthorized user </response>
        /// <response code="400">Internal server error - bad request </response>
        [Route("add-document")]
        [HttpPost]
        public async Task<IActionResult> AddDocument(AppStageDocsViewModel model) => Response(await _appDocService.Create(model));

        /// <summary>
        /// This endpoint is used to update a facility type doc to FAD Doc
        /// </summary>
        /// <returns>Returns a success message or rotherwise</returns>
        /// <remarks>
        /// 
        /// Sample Request
        /// GET: api/appstagedoc/getall
        /// 
        /// </remarks>
        /// <param name="id">This is the id of the document to be updated to FDA doc</param>
        /// <response code="200">Returns an application info </response>
        /// <response code="404">Returns not found </response>
        /// <response code="401">Unauthorized user </response>
        /// <response code="400">Internal server error - bad request </response>
        [Route("update-fad-doc")]
        [HttpGet]
        public async Task<IActionResult> UpdateFADDoc(int id) => Response(await _appDocService.UpdateFADDoc(id));
    }
}
