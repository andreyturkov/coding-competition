using CodingCompetition.Application.Interfaces;
using CodingCompetition.Application.Models.Competition;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodingCompetition.Web.Controllers
{
	public class CompetitionController : BaseApiController
	{
		private readonly ICompetitionService _competitionService;

		public CompetitionController(ICompetitionService competitionService)
		{
			_competitionService = competitionService;
		}

		[HttpPost("RunSolution")]
		public async Task<Result> RunSolution(RunRequest request)
		{
			return await _competitionService.RunSolution(request);
		}

		[HttpPost("SubmitSolution")]
		public async Task<Result> SubmitSolution(SubmitRequest request)
		{
			return await _competitionService.SubmitSolution(request);
		}
	}
}
