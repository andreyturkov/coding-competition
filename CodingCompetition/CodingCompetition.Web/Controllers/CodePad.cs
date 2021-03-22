using CodingCompetition.Application.Models;
using System;

namespace CodingCompetition.Web.Controllers
{
	public class CodePad
	{
		public string Code { get; set; }
		public string Theme { get; set; }
		public Language Language { get; set; }
		public DateTime Sent { get; set; }
	}
}
