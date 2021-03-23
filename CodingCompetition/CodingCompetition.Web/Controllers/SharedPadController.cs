using CodingCompetition.Application.Interfaces;
using CodingCompetition.Application.Models.SharedPad;
using CodingCompetition.Web.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingCompetition.Web.Controllers
{
	public class SharedPadController : BaseApiController
	{
		private readonly IHubContext<SharedPadHub> _hubContext;
		private readonly ISharedPadService _sharedPadService;

		public SharedPadController(IHubContext<SharedPadHub> hubContext, ISharedPadService sharedPadService)
		{
			_hubContext = hubContext;
			_sharedPadService = sharedPadService;
		}

		[HttpGet]
		public IList<CodePad> GetAll()
		{
			return _sharedPadService.GetAllPads();
		}

		[HttpGet("{padId}")]
		public CodePad Get(string padId)
		{
			return _sharedPadService.GetPad(padId);
		}

		[HttpPost]
		public IActionResult Create()
		{
			var id = _sharedPadService.CreatePad();
			return Ok(new { id });
		}

		[HttpPut]
		public bool Update(CodePad codePad)
		{
			return _sharedPadService.UpdatePad(codePad);
		}

		[HttpDelete("{padId}")]
		public async Task<bool> Remove(string padId)
		{
			await _hubContext.Clients.Group(padId).SendAsync("Remove", padId);

			return _sharedPadService.RemovePad(padId);
		}

		[HttpGet("{padId}/Users")]
		public IList<CodePadUser> GetUsers(string padId)
		{
			return _sharedPadService.GetPadUsers(padId);
		}
	}
}
