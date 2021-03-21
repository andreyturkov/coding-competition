using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingCompetition.Data.Interfaces;

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

		protected IQueryable<TEntity> QueryableDbSet => DbSet.AsQueryable();

		protected IQueryable<TEntity> AsNoTracking => DbSet.AsNoTracking();


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
		}

		public virtual async Task AddRange(IEnumerable<TEntity> entities)
		{
			await DbSet.AddRangeAsync(entities);
		}

		public virtual void Attach(TEntity entity)
		{
			DbSet.Attach(entity);
		}

		public virtual void Update(TEntity entity)
		{
			DbSet.Update(entity);
		}

		public virtual void UpdateRange(IEnumerable<TEntity> entities)
		{
			DbSet.UpdateRange(entities);
		}

		public bool Delete(TEntity entity)
		{
			if (Context.Entry(entity).State == EntityState.Detached)
			{
				DbSet.Attach(entity);
			}

			DbSet.Remove(entity);

			return Context.Entry(entity).State == EntityState.Deleted;
		}

		public async Task<int> ExecuteSqlCommand(string sqlCommand, object[] sqlParameters)
		{
			if (!string.IsNullOrWhiteSpace(sqlCommand))
			{
				return await Context.Database.ExecuteSqlRawAsync(sqlCommand, sqlParameters);
			}

			return 0;
		}

		public async Task<object> ExecuteSqlQuery(string sqlCommand, object[] sqlParameters = null)
		{
			if (sqlParameters == null)
			{
				return await Context.Database.ExecuteSqlRawAsync(sqlCommand);
			}

			return await Context.Database.ExecuteSqlRawAsync(sqlCommand, sqlParameters);
		}

		public async Task Reload(TEntity entity)
		{
			await Context.Entry(entity).ReloadAsync();
		}
	}
}
