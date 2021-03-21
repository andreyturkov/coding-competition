using CodingCompetition.Data.Interfaces;
using CodingCompetition.Data.Models;

namespace CodingCompetition.Data.Services
{
	internal class SolutionRepository : BaseRepository<Solution>, ISolutionRepository
	{
		public SolutionRepository(ChallengesContext context) : base(context)
		{
		}
	}
}
