namespace CodingCompetition.Application.Models
{
	public class TestResult
	{
		public int Id { get; set; }
		public int SolutionId { get; set; }
		public bool Success { get; set; }
		public string InputParameter { get; set; }
		public string ExpectedResult { get; set; }
		public string ActualResult { get; set; }


		public Solution Solution { get; set; }
	}
}
