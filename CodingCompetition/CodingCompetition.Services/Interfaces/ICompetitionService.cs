using CodingCompetition.Application.Models.Competition;
using System.Threading.Tasks;

namespace CodingCompetition.Application.Interfaces
{
	public interface ICompetitionService
	{
		Task<Result> RunSolution(RunRequest request);
		Task<Result> SubmitSolution(SubmitRequest request);
	}
}
