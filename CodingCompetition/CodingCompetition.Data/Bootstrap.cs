using CodingCompetition.Data.Interfaces;
using CodingCompetition.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CodingCompetition.Data
{
	public static class Bootstrap
	{
		public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ChallengesContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			services.AddTransient<IChallengeRepository, ChallengeRepository>();
			services.AddTransient<IPlayerRepository, PlayerRepository>();
			services.AddTransient<ISolutionRepository, SolutionRepository>();
			services.AddTransient<ITemplateRepository, TemplateRepository>();
			services.AddTransient<ITestRepository, TestRepository>();
			services.AddTransient<ITestResultRepository, TestResultRepository>();

			return services;
		}

		public static void CreateDbIfNotExists(this IServiceScope scope)
		{
			var services = scope.ServiceProvider;
			try
			{
				var context = services.GetRequiredService<ChallengesContext>();
				DbInitializer.Initialize(context);
			}
			catch (Exception ex)
			{
				var logger = services.GetRequiredService<ILogger<ChallengesContext>>();
				logger.LogError(ex, "An error occurred creating the DB.");
			}
		}
	}
}
