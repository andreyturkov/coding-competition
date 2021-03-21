using System.Collections.Generic;
using System.Threading.Tasks;
using CodingCompetition.Application.Models;

namespace CodingCompetition.Application.Interfaces
{
	public interface IChallengeService
	{
		Task<Challenge> Get(int id);
		Task<IList<Challenge>> GetAll();
	}
}
