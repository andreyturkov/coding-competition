using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingCompetition.Data.Interfaces
{
	public interface IRepository<TEntity>
	{
		Task<TEntity> Get(int id);
		Task<IList<TEntity>> GetAll();
		Task Add(TEntity entity);
		Task AddRange(IEnumerable<TEntity> entities);
		Task Update(TEntity entity);
		Task UpdateRange(IEnumerable<TEntity> entities);
		Task<bool> Delete(TEntity entity);
	}
}
