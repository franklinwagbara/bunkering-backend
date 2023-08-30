﻿using Bunkering.Access.DAL;
using Bunkering.Access.IContracts;
using Bunkering.Core.Data;
using Bunkering.Core.Utils;
using Bunkering.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Bunkering.Access.Services
{
	public class LicenseService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _contextAccessor;
		ApiResponse _response;

		public LicenseService(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
		{
			_unitOfWork = unitOfWork;
			_contextAccessor = contextAccessor;
		}


		public async Task<ApiResponse> LicenseReport(LicenseReportViewModel model)
		{
			/*if (model == null)
			{
				_response = new ApiResponse
				{
					Message = "Invalid Entry",
					StatusCode = HttpStatusCode.BadRequest,
					Success = false
				};
			}
			else if (model.StartDate == null && model.ToDate == null && model.VesselType == null)
			{
				_response = new ApiResponse
				{
					Message = "Entry is Null",
					StatusCode = HttpStatusCode.BadRequest,
					Success = false
				};

			}
			*/
			var permitReport = await _unitOfWork.Permit.Find(p => p.IssuedDate >= model.Min && p.ExpireDate <= model.Max, "Application.Facility.VesselType");

			if (permitReport.Count() > 0)
			{
				_response = new ApiResponse
				{
					Message = "Permit Report Found",
					StatusCode = HttpStatusCode.OK,
					Success = true,
					Data = permitReport.Select(p => new
					{
						p.Id,
						p.PermitNo,
						p.IssuedDate,
						p.ExpireDate,
						VesselType = p.Application.Facility.VesselType.Name,

					})
				};

				return _response;
			}
			else
			{
				_response = new ApiResponse
				{
					Message = "Permit Report Not Found",
					StatusCode = HttpStatusCode.NotFound,
					Success = false,
				};
			}

			return _response;


		}

	}
}
