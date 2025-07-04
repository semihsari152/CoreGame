using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DomainLayer.Interfaces.Repositories
{
        public interface IRepository<T> where T : BaseEntity
        {
            // Sync Methods
            T? GetById(int id);
            T? GetByIdWithDeleted(int id); // Soft deleted dahil
            IQueryable<T> GetAll();
            IQueryable<T> GetAllWithDeleted(); // Soft deleted dahil
            IQueryable<T> Find(Expression<Func<T, bool>> expression);
            T? FirstOrDefault(Expression<Func<T, bool>> expression);

            // Async Methods
            Task<T?> GetByIdAsync(int id);
            Task<T?> GetByIdWithDeletedAsync(int id);
            Task<List<T>> GetAllAsync();
            Task<List<T>> GetAllWithDeletedAsync();
            Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);
            Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);

            // Pagination
            Task<(List<T> Items, int TotalCount)> GetPagedAsync(
                int pageNumber,
                int pageSize,
                Expression<Func<T, bool>>? filter = null,
                Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                string includeProperties = "");

            // Count
            Task<int> CountAsync(Expression<Func<T, bool>>? expression = null);
            Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);

            // Add
            T Add(T entity);
            Task<T> AddAsync(T entity);
            void AddRange(IEnumerable<T> entities);
            Task AddRangeAsync(IEnumerable<T> entities);

            // Update
            T Update(T entity);
            void UpdateRange(IEnumerable<T> entities);

            // Delete
            void Delete(T entity);
            void Delete(int id);
            void DeleteRange(IEnumerable<T> entities);
            void DeleteRange(Expression<Func<T, bool>> expression);

            // Soft Delete
            Task SoftDeleteAsync(int id);
            Task SoftDeleteAsync(T entity);
            Task SoftDeleteRangeAsync(IEnumerable<T> entities);
            Task RestoreAsync(int id);
            Task RestoreAsync(T entity);

            // Bulk Operations
            Task<int> BulkUpdateAsync(Expression<Func<T, bool>> filter, Expression<Func<T, T>> updateExpression);
            Task<int> BulkDeleteAsync(Expression<Func<T, bool>> filter);
        }
    }


