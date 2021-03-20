using CodingCompetition.Application.Interfaces;
using CodingCompetition.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeResult = CodingCompetition.Application.Models.ChallengeResult;

namespace CodingCompetition.Web.Controllers
{
	public class ChallengeController : BaseApiController
	{
		private readonly IChallengeService _challengeService;

		public ChallengeController(IChallengeService challengeService)
		{
			_challengeService = challengeService;
		}

		[HttpGet("TopPlayers")]
		public async Task<IEnumerable<Player>> GetTopPlayers(int top = 3)
		{
			return await _challengeService.GetTopPlayers(top);
		}

		[HttpGet]
		public async Task<IEnumerable<Challenge>> GetChallenges()
		{
			return await _challengeService.GetChallenges();
		}

		[Route("api/[controller]/{id}")]
		[HttpPost]
		public async Task<Challenge> GetChallenge(int id)
		{
			return await _challengeService.GetChallenge(id);
		}

		[HttpPost("RunSolution")]
		public async Task<ChallengeResult> RunSolution(ChallengeSolution solution)
		{
			return await _challengeService.RunSolution(solution);
		}

		[HttpPost("SubmitSolution")]
		public async Task<ChallengeResult> SubmitSolution(ChallengeSolution solution)
		{
			return await _challengeService.SubmitSolution(solution);
		}
	}
}
