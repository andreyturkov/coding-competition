using CodingCompetition.Compiler.Interfaces;
using CodingCompetition.Compiler.Services.Rextester;
using Microsoft.Extensions.DependencyInjection;

namespace CodingCompetition.Compiler
{
	public static class Bootstrap
	{
		public static IServiceCollection AddCompilers(this IServiceCollection services)
		{
			services.AddTransient<ICodeCompiler, RextesterClient>();

			return services;
		}
	}
}
