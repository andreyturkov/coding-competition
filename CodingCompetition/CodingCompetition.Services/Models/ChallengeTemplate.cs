using CodingCompetition.Application.Config;

namespace CodingCompetition.Application.Models
{
	public class ChallengeTemplate
	{
		public int Id { get; set; }
		public SupportedLanguage Language { get; set; }
		public string TemplateCode { get; set; }
		public string CompilerAdapter { get; set; }
	}
}
