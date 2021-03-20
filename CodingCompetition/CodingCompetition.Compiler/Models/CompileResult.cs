namespace CodingCompetition.Compiler.Models
{
	public class CompileResult
	{
		public string Result { get; set; }
		public string Warnings { get; set; }
		public string Errors { get; set; }
		public string Statistic { get; set; }
	}
}