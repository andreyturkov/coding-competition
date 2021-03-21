namespace CodingCompetition.Application.Models.Competition
{
	public class SubmitRequest : Competition.RunRequest
	{
		public Player Player { get; set; }
	}
}
