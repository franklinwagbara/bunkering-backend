using Bunkering.Access;
using Bunkering.Access.IContracts;
using Bunkering.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using System.Net;

namespace Bunkering.Controllers.API
{
	[Authorize]
	public class LicensesController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public LicensesController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var permits = await _unitOfWork.Permit.GetAll("Application.User.Company,Application.Facility.FacilityType");
			if (permits.Count() > 0)
				return Ok(new ApiResponse
				{
					Message = "Success",
					StatusCode = HttpStatusCode.OK,
					Success = true,
					Data = permits.Select(x => new
					{
						CompanyName = x.Application.User.Company.Name,
						x.PermitNo,
						IssuedDate = x.IssuedDate.ToString("MMM dd, yyyy HH:mm:ss"),
						ExpiryDate = x.ExpireDate.ToString("MMM dd, yyyy HH:mm:ss"),
						x.Application.User.Email,
						VesselTypeType = x.Application.Facility.VesselType.Name,
						FacilityName = x.Application.Facility.Name,
					})
				});
			else
				return NotFound(new ApiResponse
				{
					Message = "Not found",
					StatusCode = HttpStatusCode.NotFound,
					Success = false,
				});
		}

		[HttpGet]
		public async Task<IActionResult> ViewLicense(int id)
		{
			var license = await _unitOfWork.Permit.FirstOrDefaultAsync(x => x.Id.Equals(id), "Application.User.Company");
			if (license != null)
			{
				var qrcode = Utils.GenerateQrCode($"{Request.Scheme}://{Request.Host}/License/ValidateQrCode/{license.ApplicationId}");
				license.QRCode = Convert.ToBase64String(qrcode, 0, qrcode.Length);
				var viewAsPdf = new ViewAsPdf
				{
					Model = license,
					PageHeight = 327,
					ViewName = "ViewLicense"
				};
				var pdf = await viewAsPdf.BuildFile(ControllerContext);
				return File(new MemoryStream(pdf), "application/pdf");
			}
			return BadRequest();
		}
	}
}
