using System.Collections.Generic;
using System.Threading.Tasks;
using CodingCompetition.Data.Interfaces;
using CodingCompetition.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingCompetition.Data.Services
{
	internal class ChallengeRepository : BaseRepository<Challenge>, IChallengeRepository
	{
		public ChallengeRepository(ChallengesContext context) : base(context)
		{
		}

		public override async Task<IList<Challenge>> GetAll()
		{
			return await DbSet
				.Include(x => x.Templates)
				.Include(x => x.Tests)
				.Include(x => x.Solutions)
				.ToListAsync();
		}

		public override async Task<Challenge> Get(int id)
		{
			return await DbSet
				.Include(x => x.Templates)
				.Include(x => x.Tests)
				.Include(x => x.Solutions)
				.FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
