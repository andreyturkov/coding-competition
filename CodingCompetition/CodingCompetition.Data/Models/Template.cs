namespace CodingCompetition.Data.Models
{
	public class Template
	{
		public int Id { get; set; }
		public int ChallengeId { get; set; }
		public Language Language { get; set; }
		public string TemplateCode { get; set; }
		public string CompilerAdapter { get; set; }

		public Challenge Challenge { get; set; }
	}
}
