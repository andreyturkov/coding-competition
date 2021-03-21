using CodingCompetition.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingCompetition.Data.Services
{
	internal class ChallengesContext : DbContext
	{
		public ChallengesContext(DbContextOptions<ChallengesContext> options) : base(options)
		{
		}

		public DbSet<Player> Players { get; set; }
		public DbSet<Challenge> Challenges { get; set; }
		public DbSet<Solution> Solutions { get; set; }
		public DbSet<Template> Templates { get; set; }
		public DbSet<Test> Tests { get; set; }
		public DbSet<TestResult> TestResult { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Player>().ToTable("Player");
			modelBuilder.Entity<Challenge>().ToTable("Challenge");
			modelBuilder.Entity<Solution>().ToTable("Solution");
			modelBuilder.Entity<Template>().ToTable("Template");
			modelBuilder.Entity<Test>().ToTable("Test");
			modelBuilder.Entity<TestResult>().ToTable("TestResult");
		}
	}
}