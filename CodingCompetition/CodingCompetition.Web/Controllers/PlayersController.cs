using CodingCompetition.Application.Interfaces;
using CodingCompetition.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingCompetition.Web.Controllers
{
	public class PlayersController : BaseApiController
	{
		private readonly IPlayerService _playerService;

		public PlayersController(IPlayerService playerService)
		{
			_playerService = playerService;
		}

		[HttpGet]
		public async Task<IList<Player>> GetTopPlayers(int top = 3)
		{
			return await _playerService.GetTopPlayers(top);
		}
	}
}
