using System.Collections.Generic;

namespace CodingCompetition.Application.Models
{
	public class Player
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string Nickname { get; set; }

		public ICollection<Solution> Submissions { get; set; }
	}
}
