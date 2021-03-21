using CodingCompetition.Application.Models;
using System.Threading.Tasks;

namespace CodingCompetition.Application.Interfaces
{
	public interface ISolutionService
	{
		Task AddSolution(Solution solution);
	}
}
