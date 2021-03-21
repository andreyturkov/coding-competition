using CodingCompetition.Data.Interfaces;
using CodingCompetition.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingCompetition.Data.Services
{

	internal class PlayerRepository : BaseRepository<Player>, IPlayerRepository
	{
		public PlayerRepository(ChallengesContext context) : base(context)
		{
		}

		public async Task<IList<Player>> GetTopPlayers(int take)
		{
			return await DbSet
				.Include(x => x.Submissions)
				.OrderByDescending(x => x.Submissions.Count(c => c.Success))
				.Take(take)
				.ToListAsync();
		}

		public async Task<Player> Find(string email)
		{
			return await DbSet.FirstOrDefaultAsync(x => x.Email == email);
		}
	}
}
