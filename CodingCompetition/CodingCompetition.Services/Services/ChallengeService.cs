using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingCompetition.Application.Config;
using CodingCompetition.Application.Interfaces;
using CodingCompetition.Application.Models;
using CodingCompetition.Compiler.Interfaces;
using CodingCompetition.Compiler.Models;

namespace CodingCompetition.Application.Services
{
	public class ChallengeService : IChallengeService
	{
		private const string SolutionPlaceholder = "/*SOLUTION_PLACEHOLDER*/";
		private readonly ICodeCompiler _codeCompiler;

		public ChallengeService(ICodeCompiler codeCompiler)
		{
			_codeCompiler = codeCompiler;
		}

		public Task<IEnumerable<Player>> GetTopPlayers(int top = 3)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Challenge>> GetChallenges()
		{
			return Task.FromResult(new[] { Fibonacci, MostWater }.AsEnumerable());
		}

		public async Task<Challenge> GetChallenge(int challengeId)
		{
			var challenges = await GetChallenges();

			return challenges.FirstOrDefault(x => x.Id == challengeId);
		}

		public async Task<ChallengeResult> RunSolution(ChallengeSolution solution)
		{
			var challenge = await GetChallenge(solution.ChallengeId);

			var passedTests = new List<CompileResult>();
			var failedTests = new List<ChallengeTestResult>();
			CompileResult error = null;

			foreach (var test in challenge.Tests)
			{
				var code = CreateCompileRequest(challenge, solution, test.InputParameter);
				var compileResult = await _codeCompiler.RunCode(code);

				if (!string.IsNullOrEmpty(compileResult.Errors))
				{
					error = compileResult;
					break;
				}

				if (compileResult.Result == test.ExpectedResult)
				{
					passedTests.Add(compileResult);
				}
				else
				{
					failedTests.Add(new ChallengeTestResult
					{
						Input = test.InputParameter,
						ExpectedResult = test.ExpectedResult,
						ActualResult = compileResult.Result
					});
				}
			}

			var success = passedTests.Count == challenge.Tests.Length;

			return new ChallengeResult
			{
				Solution = solution,
				Success = success,
				Message = $"Passed {passedTests.Count} of {challenge.Tests.Length} tests.",
				Errors = error?.Errors,
				FailedTests = failedTests.ToArray(),
				Runtime = passedTests.FirstOrDefault()?.Statistic,
				Warnings = string.Empty
			};
		}

		public async Task<ChallengeResult> SubmitSolution(ChallengeSolution solution)
		{
			var result = await RunSolution(solution);
			//TODO: save result;

			return result;
		}

		#region Mapping

		private static CompileRequest CreateCompileRequest(Challenge challenge, ChallengeSolution solution, string input)
		{
			var template = challenge.Templates.FirstOrDefault(x => x.Language == solution.Language);
			if (template == null)
			{
				throw new NotSupportedException($"{solution.Language.ToString()} language is not supported.");
			}

			return new CompileRequest
			{
				Language = (Language)(int)solution.Language,
				Code = template.CompilerAdapter.Replace(SolutionPlaceholder, solution.SolutionCode),
				Input = input
			};
		}

		#endregion
		#region hard-coded data

		private static Challenge Fibonacci = new Challenge
		{
			Id = 0,
			Name = "Fibonacci",
			Description = "<p>Return first N items from Fibonacci sequence.</p>",
			Templates = new[]
			{
				new ChallengeTemplate
				{
					Id = 0,
					Language = SupportedLanguage.CSharp,
					TemplateCode =
@"
public class Solution
{
	public int[] FibonacciSequence(int n)
	{
	}
}
",
					CompilerAdapter =
@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rextester
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var n = int.Parse(Console.ReadLine());
			var result = new Solution().FibonacciSequence(n);
			Console.Write(string.Join("","", result));
		}
	}

	/*SOLUTION_PLACEHOLDER*/
}
"

				},
			},
			Tests = new[]
			{
				new ChallengeTest
				{
					Id = 0,
					InputParameter = "5",
					ExpectedResult = "1,1,2,3,5"
				},
				new ChallengeTest
				{
					Id = 1,
					InputParameter = "8",
					ExpectedResult = "1,1,2,3,5,8,13,21"
				}
			}

		};

		private static Challenge MostWater = new Challenge
		{
			Id = 1,
			Name = "Container With Most Water",
			Description = @"
<p>
Given n non-negative integers a1, a2, ..., an , where each represents a point at coordinate (i, ai). n vertical lines are drawn such that the two endpoints of the line i is at (i, ai) and (i, 0). Find two lines, which, together with the x-axis forms a container, such that the container contains the most water.
</p>
<p>
Notice that you may not slant the container.
</p>
 

<h4>Example 1:</h4>
<p>
Input: height = [1,8,6,2,5,4,8,3,7]
Output: 49
Explanation: The above vertical lines are represented by array [1,8,6,2,5,4,8,3,7]. In this case, the max area of water (blue section) the container can contain is 49.
</p>

<h4>Example 2:</h4>
<p>
Input: height = [1,1]
Output: 1
</p>

<h4>Example 3:</h4>
<p>
Input: height = [4,3,2,1,4]
Output: 16
</p>

<h4>Example 4:</h4>
<p>
Input: height = [1,2,1]
Output: 2
 </p>
 
<p>
Constraints:

n == height.length
2 <= n <= 105
0 <= height[i] <= 104
</p > 
",
			Templates = new[]
			{
				new ChallengeTemplate
				{
					Id = 0,
					Language = SupportedLanguage.CSharp,
					TemplateCode =
						@"
public class Solution 
{
    public int MaxArea(int[] height) 
	{
        
    }
}
",
					CompilerAdapter =
						@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rextester
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var height = Console.ReadLine().Split(',').Select(int.Parse).ToArray();
			var result = new Solution().MaxArea(height);
			Console.Write(string.Join("","", result));
		}
	}

	/*SOLUTION_PLACEHOLDER*/
}
"

				},
			},
			Tests = new[]
			{
				new ChallengeTest
				{
					Id = 0,
					InputParameter = "1,8,6,2,5,4,8,3,7",
					ExpectedResult = "49"
				},
				new ChallengeTest
				{
					Id = 1,
					InputParameter = "1,1",
					ExpectedResult = "1"
				},
				new ChallengeTest
				{
					Id = 2,
					InputParameter = "4,3,2,1,4",
					ExpectedResult = "16"
				},
				new ChallengeTest
				{
					Id = 3,
					InputParameter = "1,2,1",
					ExpectedResult = "2"
				}
			}

		};

		#endregion
	}
}


