using CodingCompetition.Data.Interfaces;
using CodingCompetition.Data.Models;

namespace CodingCompetition.Data.Services
{
	internal class TestResultRepository : BaseRepository<TestResult>, ITestResultRepository
	{
		public TestResultRepository(ChallengesContext context) : base(context)
		{
		}
	}
}

