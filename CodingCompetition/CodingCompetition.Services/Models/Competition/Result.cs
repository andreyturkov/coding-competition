using System.Collections.Generic;

namespace CodingCompetition.Application.Models.Competition
{
	public class Result
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public string Runtime { get; set; }
		public string Warnings { get; set; }
		public string Errors { get; set; }

		public ICollection<TestResult> TestResults { get; set; }
	}
}
