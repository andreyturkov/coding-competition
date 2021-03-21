using AutoMapper;
using CodingCompetition.Application.Interfaces;
using CodingCompetition.Application.Models;
using CodingCompetition.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingCompetition.Application.Services
{
	internal class TestResultService : ITestResultService
	{
		private readonly ITestResultRepository _testResultRepository;
		private readonly IMapper _mapper;

		public TestResultService(ITestResultRepository testResultRepository, IMapper mapper)
		{
			_testResultRepository = testResultRepository;
			_mapper = mapper;
		}

		public async Task AddTestResults(IEnumerable<TestResult> testResults)
		{
			var entities = _mapper.Map<IList<Data.Models.TestResult>>(testResults);
			await _testResultRepository.AddRange(entities);
		}
	}
}
