namespace CodingCompetition.Compiler.Models
{
	public class CompileRequest
	{
		public Language Language { get; set; }
		public string Code { get; set; }
		public string Input { get; set; }
		public string CompilerArgs { get; set; }
	}
}