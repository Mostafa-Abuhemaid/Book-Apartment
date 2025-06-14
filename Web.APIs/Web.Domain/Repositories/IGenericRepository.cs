using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Entites;

namespace Web.Domain.Repositories
{
	public interface IGenericRepository<TKey, TEntity> where TEntity : BaseClass<TKey>
	{
		Task AddAsync(TEntity entity);
		Task<TEntity?> GetByIdAsync(TKey id);
		Task<TEntity?> GetEntityWithPrdicateAsync(Expression<Func<TEntity, bool>> predicate);
		void Remove(TEntity entity);
		Task<IReadOnlyList<TEntity>?> GetWithPrdicateAsync(Expression<Func<TEntity, bool>> pridecate);

	}
}
