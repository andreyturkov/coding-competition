namespace CodingCompetition.Application.Models
{
	public class Challenge
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public ChallengeTemplate[] Templates { get; set; }
		public ChallengeTest[] Tests { get; set; }
	}
}
