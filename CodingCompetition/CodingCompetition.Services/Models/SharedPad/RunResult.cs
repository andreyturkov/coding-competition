namespace CodingCompetition.Application.Models.SharedPad
{
	public class RunResult
	{
		public bool Success { get; set; }
		public string Result { get; set; }
		public string Warnings { get; set; }
		public string Errors { get; set; }
		public string Statistic { get; set; }
	}
}
