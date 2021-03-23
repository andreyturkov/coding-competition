using CodingCompetition.Application.Interfaces;
using CodingCompetition.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CodingCompetition.Application
{
	public static class Bootstrap
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddSingleton(AutoMapperConfiguration.Configure().CreateMapper());

			services.AddTransient<ISolutionService, SolutionService>();
			services.AddTransient<ITestResultService, TestResultService>();
			services.AddTransient<IChallengeService, ChallengeService>();
			services.AddTransient<IPlayerService, PlayerService>();
			services.AddTransient<ISharedPadService, SharedPadService>();
			services.AddTransient<ISharedCodePadFactory, SharedCodePadFactory>();

			services.AddTransient<ICompetitionService, CompetitionService>();

			return services;
		}
	}
}
