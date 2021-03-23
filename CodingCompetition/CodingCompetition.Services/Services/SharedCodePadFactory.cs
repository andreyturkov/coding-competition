using CodingCompetition.Application.Models;
using CodingCompetition.Application.Models.SharedPad;
using System;
using CodingCompetition.Application.Interfaces;

namespace CodingCompetition.Application.Services
{
	public class SharedCodePadFactory : ISharedCodePadFactory
	{
		public CodePad CreateCSharp()
		{
			return new CodePad
			{
				Id = Guid.NewGuid().ToString("N").ToUpper().Substring(0, 10),
				Language = Language.CSharp,
				Theme = "eclipse",
				Code = @"
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
			Console.Write(""Hello World"");
		}
	}
}"
			};
		}
	}
}
