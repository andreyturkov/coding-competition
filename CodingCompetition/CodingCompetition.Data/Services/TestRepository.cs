using CodingCompetition.Data.Interfaces;
using CodingCompetition.Data.Models;

namespace CodingCompetition.Data.Services
{
	internal class TestRepository : BaseRepository<Test>, ITestRepository
	{
		public TestRepository(ChallengesContext context) : base(context)
		{
		}
	}
}
