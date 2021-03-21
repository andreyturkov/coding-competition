using System.Collections.Generic;

namespace CodingCompetition.Application.Models
{
	public class Challenge
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public ICollection<Template> Templates { get; set; }
		public ICollection<Test> Tests { get; set; }
		public ICollection<Solution> Solutions { get; set; }
	}
}
