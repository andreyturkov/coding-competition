using CodingCompetition.Compiler.Models;
using System.Threading.Tasks;

namespace CodingCompetition.Compiler.Interfaces
{
	public interface ICodeCompiler
	{
		Task<CompileResult> RunCode(CompileRequest code);
	}
}
