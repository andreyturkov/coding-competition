namespace CodingCompetition.Application.Models.SharedPad
{
	public class CodePadUser
	{
		public CodePadUser(string id, string name)
		{
			Id = id;
			Name = name;
		}

		public string Id { get; }
		public string Name { get; }
	}
}
