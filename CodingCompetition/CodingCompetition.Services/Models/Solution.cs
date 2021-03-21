using System.Collections.Generic;

namespace CodingCompetition.Application.Models
{
	public class Solution
	{
		public int Id { get; set; }
		public int PlayerId { get; set; }
		public int ChallengeId { get; set; }
		public Language Language { get; set; }
		public string SolutionCode { get; set; }
		public bool Success { get; set; }
		public string Message { get; set; }
		public string Runtime { get; set; }
		public string Warnings { get; set; }
		public string Errors { get; set; }


		public Player Player { get; set; }
		public Challenge Challenge { get; set; }
		public ICollection<TestResult> TestResults { get; set; }
	}
}
