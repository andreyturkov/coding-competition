using CodingCompetition.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingCompetition.Data.Services
{
	internal abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected readonly ChallengesContext Context;
		protected readonly DbSet<TEntity> DbSet;

		protected BaseRepository(ChallengesContext context)
		{
			Context = context;
			DbSet = context.Set<TEntity>();
		}

		public virtual async Task<TEntity> Get(int id)
		{
			return await DbSet.FindAsync(id);
		}

		public virtual async Task<IList<TEntity>> GetAll()
		{
			return await DbSet.ToListAsync();
		}

		public virtual async Task Add(TEntity entity)
		{
			await DbSet.AddAsync(entity);
			await Context.SaveChangesAsync();
		}

		public virtual async Task AddRange(IEnumerable<TEntity> entities)
		{
			await DbSet.AddRangeAsync(entities);
			await Context.SaveChangesAsync();
		}

		public virtual async Task Update(TEntity entity)
		{
			DbSet.Update(entity);
			await Context.SaveChangesAsync();
		}

		public virtual async Task UpdateRange(IEnumerable<TEntity> entities)
		{
			DbSet.UpdateRange(entities);
			await Context.SaveChangesAsync();
		}

		public async Task<bool> Delete(TEntity entity)
		{
			if (Context.Entry(entity).State == EntityState.Detached)
			{
				DbSet.Attach(entity);
			}

			DbSet.Remove(entity);
			await Context.SaveChangesAsync();

			return Context.Entry(entity).State == EntityState.Deleted;
		}
	}
}
