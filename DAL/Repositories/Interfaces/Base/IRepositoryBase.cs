﻿using Ardalis.Specification;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace NET_Core_Task.DAL.Repositories.Interfaces.Base
{
    public interface IRepositoryBase<T>
    where T : class
    {
        IQueryable<T> FindAll(Expression<Func<T, bool>>? predicate = default);
        T Create(T entity);

        Task<T> CreateAsync(T entity);

        Task CreateRangeAsync(IEnumerable<T> items);

        EntityEntry<T> Update(T entity);

        public void UpdateRange(IEnumerable<T> items);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> items);

        void Attach(T entity);

        void Detach(T entity);

        EntityEntry<T> Entry(T entity);

        public Task ExecuteSqlRaw(string query, params object[] parameters);

        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);

        Task<IEnumerable<T>?> GetAllAsync(
            Expression<Func<T, T>> selector,
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);
        Task<IEnumerable<T>?> GetAllWithSpecAsync(params ISpecification<T>[] specs);

        Task<T?> GetSingleOrDefaultAsync(
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);
        Task<T?> GetSingleOrDefaultWithSpecAsync(params ISpecification<T>[] specs);

        Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);

        Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, T>> selector,
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);
        Task<T?> GetFirstOrDefaultWithSpecAsync(params ISpecification<T>[] specs);
    }
}
