using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bunkering.Controllers
{

	[Authorize]
	[ApiController]
	[Route("api/bunkering/[controller]")]
	public class GeneralReport : ResponseController
	{
	}
}
