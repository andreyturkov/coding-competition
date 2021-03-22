using CodingCompetition.Web.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading.Tasks;
using CodingCompetition.Web.Util;

namespace CodingCompetition.Web.Controllers
{
	public class SharedPadController : BaseApiController
	{
		private readonly MemoryCacheEntryOptions _cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(180));
		private readonly IHubContext<SharedPadHub> _hubContext;
		private readonly IMemoryCache _cache;

		public SharedPadController(IHubContext<SharedPadHub> hubContext, IMemoryCache cache)
		{
			_hubContext = hubContext;
			_cache = cache;
		}

		[HttpGet("{id}")]
		public CodePad Get(string id)
		{
			return _cache.Get<CodePad>($"{id}");
		}

		[HttpGet]
		public string[] GetAll()
		{
			return _cache.GetKeys<string>().ToArray();
		}

		[HttpPost]
		public IActionResult CreatePad(CodePad codePad)
		{
			var id = Guid.NewGuid().ToString("N").ToUpper().Substring(0, 10);
			_cache.Set($"{id}", codePad, _cacheOptions);
			return Ok(new { id });
		}

		[HttpPut("{id}")]
		public bool Save(string id, [FromBody]CodePad codePad)
		{
			_cache.Set($"{id}", codePad, _cacheOptions);
			return true;
		}

		[HttpDelete("{id}")]
		public async Task<bool> Remove(string id)
		{
			_cache.Remove($"{id}");
			await _hubContext.Clients.Group(id).SendAsync("RemoveCodePad", id);
			return true;
		}

		[HttpGet("Users/{id}")]
		public string[] GetUsers(string id)
		{
			return new string[0];
		}
	}
}
