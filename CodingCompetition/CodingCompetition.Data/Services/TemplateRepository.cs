using CodingCompetition.Data.Interfaces;
using CodingCompetition.Data.Models;

namespace CodingCompetition.Data.Services
{
	internal class TemplateRepository : BaseRepository<Template>, ITemplateRepository
	{
		public TemplateRepository(ChallengesContext context) : base(context)
		{
		}
	}
}
