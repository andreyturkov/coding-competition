using System.Threading.Tasks;
using AutoMapper;
using CodingCompetition.Application.Interfaces;
using CodingCompetition.Application.Models;
using CodingCompetition.Data.Interfaces;

namespace CodingCompetition.Application.Services
{
	internal class SolutionService : ISolutionService
	{
		private readonly ISolutionRepository _solutionRepository;
		private readonly ITestResultService _testResultService;
		private readonly IMapper _mapper;

		public SolutionService(ISolutionRepository solutionRepository, ITestResultService testResultService, IMapper mapper)
		{
			_solutionRepository = solutionRepository;
			_testResultService = testResultService;
			_mapper = mapper;
		}

		public async Task AddSolution(Solution solution)
		{
			var entity = _mapper.Map<Data.Models.Solution>(solution);
			await _solutionRepository.Add(entity);

			foreach (var testResult in solution.TestResults)
			{
				testResult.SolutionId = entity.Id;
			}

			await _testResultService.AddTestResults(solution.TestResults);
		}
	}
}
