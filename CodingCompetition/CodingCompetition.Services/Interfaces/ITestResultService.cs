using CodingCompetition.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingCompetition.Application.Interfaces
{
	public interface ITestResultService
	{
		Task AddTestResults(IEnumerable<TestResult> testResults);
	}
}
