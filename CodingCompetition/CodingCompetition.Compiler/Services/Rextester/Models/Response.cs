namespace CodingCompetition.Compiler.Services.Rextester.Models
{
	public class Response
	{
		public string Result { get; set; }
		public string Warnings { get; set; }
		public string Errors { get; set; }
		public string Stats { get; set; }
	}
}