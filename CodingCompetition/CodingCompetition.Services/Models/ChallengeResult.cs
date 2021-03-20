using System;

namespace CodingCompetition.Application.Models
{
	public class ChallengeResult
	{
		public int Id { get; set; }
		public ChallengeSolution Solution { get; set; }
		public string Runtime { get; set; }
		public bool Success { get; set; }
		public ChallengeTestResult[] FailedTests { get; set; }
		public string Warnings { get; set; }
		public string Errors { get; set; }
		public string Message { get; set; }
	}
}
