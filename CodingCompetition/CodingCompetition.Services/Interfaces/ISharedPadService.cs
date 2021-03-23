using CodingCompetition.Application.Models.SharedPad;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingCompetition.Application.Interfaces
{
	public interface ISharedPadService
	{
		string CreatePad();
		bool UpdatePad(CodePad pad);
		bool RemovePad(string padId);
		IList<CodePad> GetAllPads();
		CodePad GetPad(string padId);

		bool AddPadUser(string padId, CodePadUser user);
		bool RemovePadUser(string padId, string userId);
		IList<CodePadUser> GetPadUsers(string padId);

		Task<RunResult> Run(CodePad pad);
	}
}
