namespace CodingCompetition.Application.Models
{
	public class Test
	{
		public int Id { get; set; }
		public int ChallengeId { get; set; }
		public string InputParameter { get; set; }
		public string ExpectedResult { get; set; }

		public Challenge Challenge { get; set; }
	}
}
