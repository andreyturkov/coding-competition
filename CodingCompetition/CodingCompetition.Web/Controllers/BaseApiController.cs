using Microsoft.AspNetCore.Mvc;

namespace CodingCompetition.Web.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public abstract class BaseApiController : ControllerBase
	{
	}
}