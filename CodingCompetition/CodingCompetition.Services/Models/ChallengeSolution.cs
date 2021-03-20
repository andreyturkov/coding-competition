using CodingCompetition.Application.Config;

namespace CodingCompetition.Application.Models
{
	public class ChallengeSolution
	{
		public int Id { get; set; }
		public Player Player { get; set; }
		public int ChallengeId { get; set; }
		public SupportedLanguage Language { get; set; }
		public string SolutionCode { get; set; }
	}
}
