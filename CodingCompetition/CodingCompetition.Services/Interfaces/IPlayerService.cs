using CodingCompetition.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingCompetition.Application.Interfaces
{
	public interface IPlayerService
	{
		Task<IList<Player>> GetTopPlayers(int take);
		Task EnsureUserExists(Player player);
	}
}
