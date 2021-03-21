using CodingCompetition.Application.Interfaces;
using CodingCompetition.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingCompetition.Web.Controllers
{
	public class ChallengeController : BaseApiController
	{
		private readonly IChallengeService _challengeService;

		public ChallengeController(IChallengeService challengeService)
		{
			_challengeService = challengeService;
		}

		[HttpGet]
		public async Task<IList<Challenge>> GetAll()
		{
			return await _challengeService.GetAll();
		}

		[HttpGet("{id}")]
		public async Task<Challenge> Get(int id)
		{
			return await _challengeService.Get(id);
		}
	}
}
