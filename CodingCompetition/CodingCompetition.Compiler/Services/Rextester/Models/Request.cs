using CodingCompetition.Compiler.Models;

namespace CodingCompetition.Compiler.Services.Rextester.Models
{
	public class Request
	{
		public int LanguageChoice { get; set; }
		public string Program { get; set; }
		public string Input { get; set; }
		public string CompilerArgs { get; set; }
	}
}