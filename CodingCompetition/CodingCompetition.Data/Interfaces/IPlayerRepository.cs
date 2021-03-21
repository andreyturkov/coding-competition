using System.Collections.Generic;
using System.Threading.Tasks;
using CodingCompetition.Data.Models;

namespace CodingCompetition.Data.Interfaces
{
	public interface IPlayerRepository : IRepository<Player>
	{
		Task<IList<Player>> GetTopPlayers(int take);
		Task<Player> Find(string email);
	}
}
