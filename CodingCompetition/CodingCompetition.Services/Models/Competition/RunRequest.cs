namespace CodingCompetition.Application.Models.Competition
{
	public class RunRequest
	{
		public int ChallengeId { get; set; }
		public Language Language { get; set; }
		public string SolutionCode { get; set; }
	}
}
