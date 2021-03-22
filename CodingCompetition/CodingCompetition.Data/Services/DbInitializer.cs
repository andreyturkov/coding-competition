using CodingCompetition.Data.Models;
using System.Linq;

namespace CodingCompetition.Data.Services
{

	internal static class DbInitializer
	{
		public static void Initialize(ChallengesContext context)
		{
			context.Database.EnsureCreated();

			// Look for any Challenges.
			if (context.Challenges.Any())
			{
				return; // DB has been seeded
			}

			var challenges = new[]
			{
				new Challenge
				{
					Name = "Hello World",
					Description = @"<p>Return ""Hello World""</p>"
				},
				new Challenge
				{
					Name = "Fibonacci",
					Description = "<p>Return first N items from Fibonacci sequence.</p>"
				},
				new Challenge
				{
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
Input: height = [1,2,1]
Output: 2
 </p>
 
<p>
<h5>Constraints:</h5>

n == height.length
2 <= n <= 105
0 <= height[i] <= 104
</p > 
",
				},

			};
			foreach (var c in challenges)
			{
				context.Challenges.Add(c);
			}

			context.SaveChanges();

			var templates = new[]
			{
				new Template
				{
					ChallengeId = 1,
					Language = Language.CSharp,
					TemplateCode = @"
public class Solution
{
	public string HelloWorld()
	{
	}
}
",
					CompilerAdapter = @"
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
			var result = new Solution().HelloWorld();
			Console.Write(result);
		}
	}

	/*SOLUTION_PLACEHOLDER*/
}
"
				},
				new Template
				{
					ChallengeId = 1,
					Language = Language.Java,
					TemplateCode = @"
class Solution
{
	public String helloWorld()
	{
	}
}
",
					CompilerAdapter = @"
import java.util.*;
import java.lang.*;

class Rextester
{  
	public static void main(String args[])
	{
		String result = new Solution().helloWorld();
		System.out.print(result);
	}
}   

/*SOLUTION_PLACEHOLDER*/
"
				},
				new Template
				{
					ChallengeId = 2,
					Language = Language.CSharp,
					TemplateCode = @"
public class Solution
{
	public int[] FibonacciSequence(int n)
	{
	}
}
",
					CompilerAdapter = @"
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
				new Template
				{
					ChallengeId = 3,
					Language = Language.CSharp,
					TemplateCode = @"
public class Solution 
{
    public int MaxArea(int[] height) 
	{
        
    }
}
",
					CompilerAdapter = @"
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

			};

			foreach (var t in templates)
			{
				context.Templates.Add(t);
			}

			context.SaveChanges();

			var tests = new[]
			{
				new Test {ChallengeId = 1, InputParameter = "", ExpectedResult = "Hello World"},
				new Test {ChallengeId = 2, InputParameter = "5", ExpectedResult = "1,1,2,3,5"},
				new Test {ChallengeId = 2, InputParameter = "8", ExpectedResult = "1,1,2,3,5,8,13,21"},
				new Test {ChallengeId = 3, InputParameter = "1,8,6,2,5,4,8,3,7", ExpectedResult = "49"},
				new Test {ChallengeId = 3, InputParameter = "1,1", ExpectedResult = "1"},
				new Test {ChallengeId = 3, InputParameter = "4,3,2,1,4", ExpectedResult = "16"},
				new Test {ChallengeId = 3, InputParameter = "1,2,1", ExpectedResult = "2"},
			};
			foreach (var t in tests)
			{
				context.Tests.Add(t);
			}

			context.SaveChanges();
		}
	}
}