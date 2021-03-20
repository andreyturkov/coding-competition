using CodingCompetition.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingCompetition.Application.Interfaces
{
	public interface IChallengeService
	{
		Task<IEnumerable<Player>> GetTopPlayers(int top = 3);
		Task<IEnumerable<Challenge>> GetChallenges();
		Task<Challenge> GetChallenge(int challengeId);
		Task<ChallengeResult> RunSolution(ChallengeSolution solution);
		Task<ChallengeResult> SubmitSolution(ChallengeSolution solution);
	}
}
