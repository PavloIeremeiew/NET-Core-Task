﻿using Ardalis.Specification.EntityFrameworkCore;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using NET_Core_Task.DAL.Persistence;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;

namespace NET_Core_Task.DAL.Repositories.Realizations.Base
{
    public class RepositoryBase<T> : Interfaces.Base.IRepositoryBase<T>, IUniversityDBContextProvider
        where T : class
    {
        private UniversityDBContext _dbContext = null!;

        protected RepositoryBase(UniversityDBContext context)
        {
            _dbContext = context;
        }

        protected RepositoryBase()
        {
        }

        public UniversityDBContext DbContext { init => _dbContext = value; }

        public IQueryable<T> FindAll(Expression<Func<T, bool>>? predicate = default)
        {
            return GetQueryable(predicate).AsNoTracking();
        }

        public T Create(T entity)
        {
            return _dbContext.Set<T>().Add(entity).Entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var tmp = await _dbContext.Set<T>().AddAsync(entity);
            return tmp.Entity;
        }

        public Task CreateRangeAsync(IEnumerable<T> items)
        {
            return _dbContext.Set<T>().AddRangeAsync(items);
        }

        public EntityEntry<T> Update(T entity)
        {
            return _dbContext.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> items)
        {
            _dbContext.Set<T>().UpdateRange(items);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> items)
        {
            _dbContext.Set<T>().RemoveRange(items);
        }

        public void Attach(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
        }

        public EntityEntry<T> Entry(T entity)
        {
            return _dbContext.Entry(entity);
        }

        public void Detach(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        public Task ExecuteSqlRaw(string query, params object[] parameters)
        {
            return _dbContext.Database.ExecuteSqlRawAsync(query, parameters);
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            IIncludableQueryable<T, object>? query = default;

            if (includes.Any())
            {
                query = _dbContext.Set<T>().Include(includes[0]);
            }

            for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
            {
                query = query!.Include(includes[queryIndex]);
            }

            return (query is null) ? _dbContext.Set<T>() : query.AsQueryable();
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
        {
            return await GetQueryable(predicate, include).ToListAsync();
        }

        public async Task<IEnumerable<T>?> GetAllAsync(
            Expression<Func<T, T>> selector,
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
        {
            return await GetQueryable(predicate, include, selector).ToListAsync() ?? new List<T>();
        }

        public async Task<IEnumerable<T>?> GetAllWithSpecAsync(params ISpecification<T>[] specs)
        {
            return await ApplySpecifications(specs)
                .ToListAsync();
        }

        public async Task<T?> GetSingleOrDefaultAsync(
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
        {
            return await GetQueryable(predicate, include).SingleOrDefaultAsync();
        }

        public async Task<T?> GetSingleOrDefaultWithSpecAsync(params ISpecification<T>[] specs)
        {
            return await ApplySpecifications(specs)
                .SingleOrDefaultAsync();
        }

        public async Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
        {
            return await GetQueryable(predicate, include).FirstOrDefaultAsync();
        }

        public async Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, T>> selector,
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
        {
            return await GetQueryable(predicate, include, selector).FirstOrDefaultAsync();
        }

        public async Task<T?> GetFirstOrDefaultWithSpecAsync(params ISpecification<T>[] specs)
        {
            return await ApplySpecifications(specs)
                .FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecifications(params ISpecification<T>[] specs)
        {
            var query = _dbContext.Set<T>().AsQueryable().AsNoTracking();

            foreach (var spec in specs)
            {
                query = SpecificationEvaluator.Default.GetQuery(query, spec);
            }

            return query;
        }

        private IQueryable<T> GetQueryable(
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default,
            Expression<Func<T, T>>? selector = default)
        {
            var query = _dbContext.Set<T>().AsNoTracking();

            if (include is not null)
            {
                query = include(query);
            }

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            if (selector is not null)
            {
                query = query.Select(selector);
            }

            return query.AsNoTracking();
        }
    }

}
