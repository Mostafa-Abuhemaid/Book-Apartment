using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Web.Domain.Entites;
using Web.Domain.Repositories;
using Web.Infrastructure.Data;


namespace BytesMaestros.Persistence.Repositories
{
	public class GenericRepository<TKey, TEntity> : IGenericRepository<TKey, TEntity> where TEntity : BaseClass<TKey>
	{
		private readonly AppDbContext _context;
		private readonly DbSet<TEntity> _entities;
		public GenericRepository(AppDbContext context)
		{
			_context = context;
			_entities = context.Set<TEntity>();
		}
		public async Task AddAsync(TEntity entity)
		   => await _entities.AddAsync(entity);

		public async Task<TEntity?> GetByIdAsync(TKey id)
		  => await _entities.FindAsync(id)!;

		public Task<TEntity?> GetEntityWithPrdicateAsync(Expression<Func<TEntity, bool>> predicate)
		  => _entities.FirstOrDefaultAsync(predicate);

		public void Remove(TEntity entity)
		  => _entities.Remove(entity);

		public async Task<IReadOnlyList<TEntity>?> GetWithPrdicateAsync(Expression<Func<TEntity, bool>> pridecate)
		=> await _entities.Where(pridecate).ToListAsync();

	}

}
