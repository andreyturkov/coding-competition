using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingCompetition.Data.Interfaces
{
	public interface IRepository<TEntity>
	{
		void Attach(TEntity entity);

		Task<TEntity> Get(int id);
		Task<IList<TEntity>> GetAll();
		Task Add(TEntity entity);
		Task AddRange(IEnumerable<TEntity> entities);
		void Update(TEntity entity);
		void UpdateRange(IEnumerable<TEntity> entities);
		bool Delete(TEntity entity);

		Task<int> ExecuteSqlCommand(string sqlCommand, object[] sqlParameters);
		Task<object> ExecuteSqlQuery(string sqlCommand, object[] sqlParameters = null);
		Task Reload(TEntity entity);
	}
}
