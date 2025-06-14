using BytesMaestros.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Entites;
using Web.Domain.Repositories;
using Web.Infrastructure.Data;


namespace Web.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _context;
		private readonly Dictionary<string, object> _repositories = new();

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
		}

		public IGenericRepository<TKey, TEntity> Repository<TKey, TEntity>() where TEntity : BaseClass<TKey>
		{
			var typeName = typeof(TEntity).Name;

			if (!_repositories.ContainsKey(typeName))
			{
				var repositoryInstance = new GenericRepository<TKey, TEntity>(_context);
				_repositories[typeName] = repositoryInstance;
			}

			return (IGenericRepository<TKey, TEntity>)_repositories[typeName];
		}

		public async ValueTask DisposeAsync()
		{
			await _context.DisposeAsync();
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}
