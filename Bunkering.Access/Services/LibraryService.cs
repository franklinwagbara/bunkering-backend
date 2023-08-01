using Azure;
using Bunkering.Access.DAL;
using Bunkering.Access.IContracts;
using Bunkering.Core.Data;
using Bunkering.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bunkering.Access.Services
{
	public class LibraryService
	{
		private readonly IUnitOfWork unitOfWork_;
		private readonly RoleManager<ApplicationRole> _roleManager;

		ApiResponse response;

		public LibraryService(IUnitOfWork unitOfWork, RoleManager<ApplicationRole> roleManager)
		{
			unitOfWork_ = unitOfWork;
			_roleManager = roleManager;
		}

		//method to get all states
		public async Task<ApiResponse> GetAllStates()
		{
			try
			{
				//get states from db
				var allStates = (await unitOfWork_.State.GetAll()).Select(s => new StatesViewModel
				{
					Name = s.Name,
					Code = s.Code,
					Id = s.Id,
					CountryID = s.CountryId,
					CountryName = s.Name
				}).OrderBy(a => a.Name);

				if (allStates != null)
				{
					//    foreach (var state in allStates)
					//    {
					//        //create an empty object
					//        var newState = new StatesViewModel();


					//        //assign values from state to the new empty object
					//        newState.Name = state.Name;
					//        newState.Id = state.Id;
					//        newState.Code = state.Code;


					//        //add the populated object to the list
					//        stateList.Add(newState);
					//    }

					response = new ApiResponse()
					{
						Message = "Success",
						StatusCode = HttpStatusCode.OK,
						Success = true,
						Data = allStates.ToArray()
					};


				}
				else
				{
					response = new ApiResponse
					{
						Success = false,
						StatusCode = HttpStatusCode.BadRequest,
						Message = "Invalid Data Call"
					};
				}

			}
			catch (Exception ex)
			{

				response = new ApiResponse
				{
					Message = ex.Message,
					StatusCode = HttpStatusCode.InternalServerError,
				};
			}


			return response;


		}

		public async Task<ApiResponse> GetLocalGov()

		{

			try
			{
				var ii = (await unitOfWork_.LGA.GetAll());
				var all_LG = (await unitOfWork_.LGA.GetAll()).Select(se => new LGViewModel

				{
					Name = se.Name,
					StateId = se.StateId,
					Id = se.Id
				}).OrderBy(b => b.Name);

				if (all_LG != null)
				{
					response = new ApiResponse()
					{
						Message = "Success",
						StatusCode = HttpStatusCode.OK,
						Success = true,
						Data = all_LG.ToArray()
					};
				}
				else
				{
					response = new ApiResponse
					{
						Success = false,
						StatusCode = HttpStatusCode.BadRequest,
						Message = "Invalid Data Call"
					};
				}
			}
			catch (Exception ex)
			{
				response = new ApiResponse
				{
					Message = ex.Message,
					StatusCode = HttpStatusCode.InternalServerError,
				};
			}


			return response;


		}

		public async Task<ApiResponse> LGA_StateID(int state_id)
		{
			var LG_By_StateID = (await unitOfWork_.LGA.Find(y => y.StateId == state_id)).Select(lg => new LGViewModel
			{
				Id = lg.Id,
				Name = lg.Name,
				StateId = lg.StateId
			}).OrderBy(c => c.Name);

			try
			{
				if (LG_By_StateID != null)
				{
					response = new ApiResponse()
					{
						Message = "Success",
						StatusCode = HttpStatusCode.OK,
						Success = true,
						Data = LG_By_StateID.ToArray()
					};

				}
				else
				{
					response = new ApiResponse
					{
						Success = false,
						StatusCode = HttpStatusCode.BadRequest,
						Message = "Invalid Data Call"
					};
				}


			}
			catch (Exception ex)
			{
				response = new ApiResponse
				{
					Message = ex.Message,
					StatusCode = HttpStatusCode.InternalServerError,
				};


			}

			return response;
		}

		public async Task<ApiResponse> GetCountries()
		{
			try
			{
				var allcountries = (await unitOfWork_.Country.GetAll()).Select(ac => new CountryViewModel
				{
					Id = ac.Id,
					Name = ac.Name
				}).OrderBy(u => u.Name);

				if (allcountries != null)
				{
					response = new ApiResponse()
					{
						Message = "Success",
						StatusCode = HttpStatusCode.OK,
						Success = true,
						Data = allcountries.ToArray()
					};

				}
				else
				{
					response = new ApiResponse
					{
						Success = false,
						StatusCode = HttpStatusCode.BadRequest,
						Message = "Invalid Data Call"
					};
				}
			}
			catch (Exception ex)
			{
				response = new ApiResponse
				{
					Message = ex.Message,
					StatusCode = HttpStatusCode.InternalServerError,
				};

			}

			return response;


		}

		public async Task<ApiResponse> State_CountryID(int countryId)
		{
			var stateByCountryId = (await unitOfWork_.State.Find(q => q.CountryId == countryId)).Select(ci => new CountryViewModel
			{
				Id = ci.Id,
				Name = ci.Name

			}).OrderBy(c => c.Id);

			try
			{
				if (stateByCountryId != null)
				{
					response = new ApiResponse()
					{
						Message = "Success",
						StatusCode = HttpStatusCode.OK,
						Success = true,
						Data = stateByCountryId.ToArray()
					};

				}
				else
				{
					response = new ApiResponse
					{
						Success = false,
						StatusCode = HttpStatusCode.BadRequest,
						Message = "Invalid Data Call"
					};
				}


			}
			catch (Exception ex)
			{
				response = new ApiResponse
				{
					Message = ex.Message,
					StatusCode = HttpStatusCode.InternalServerError,
				};

			}

			return response;

		}

		public async Task<ApiResponse> ApplicationType()
		{
			var appType = (await unitOfWork_.ApplicationType.GetAll());

			return new ApiResponse
			{
				Data = appType,
				Message = "All Application-Type",
				StatusCode = HttpStatusCode.OK,
				Success = true,
			};
		}

		public async Task<ApiResponse> FacilityTypes()
		{
			var facilityTypes = await unitOfWork_.FacilityType.GetAll();
			return new ApiResponse
			{
				Data = facilityTypes,
				Message = "All Facility-Types",
				StatusCode = HttpStatusCode.OK,
				Success = true,
			};
		}

		public async Task<ApiResponse> GetRoles()
		{
			var roles = await _roleManager.Roles.ToListAsync();
			return new ApiResponse
			{
				Data = roles,
				Message = "Roles",
				StatusCode = HttpStatusCode.OK,
				Success = true,
			};
		}

	}
}
