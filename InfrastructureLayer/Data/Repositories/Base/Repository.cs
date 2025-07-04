using DomainLayer.Common;
using DomainLayer.Interfaces.Repositories;
using InfrastructureLayer.Data.Context;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace InfrastructureLayer.Data.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly CoreGameDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(CoreGameDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        #region Sync Methods

        public virtual T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual T? GetByIdWithDeleted(int id)
        {
            return _dbSet.IgnoreQueryFilters().FirstOrDefault(x => x.Id == id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public virtual IQueryable<T> GetAllWithDeleted()
        {
            return _dbSet.IgnoreQueryFilters().AsQueryable();
        }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public virtual T? FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression);
        }

        #endregion

        #region Async Methods

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T?> GetByIdWithDeletedAsync(int id)
        {
            return await _dbSet.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<List<T>> GetAllWithDeletedAsync()
        {
            return await _dbSet.IgnoreQueryFilters().ToListAsync();
        }

        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        #endregion

        #region Pagination

        public virtual async Task<(List<T> Items, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            // Include properties
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            // Apply filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // Apply ordering
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {
                query = query.OrderByDescending(x => x.CreatedDate);
            }

            // Apply pagination
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        #endregion

        #region Count & Exists

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? expression = null)
        {
            if (expression == null)
                return await _dbSet.CountAsync();

            return await _dbSet.CountAsync(expression);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        #endregion

        #region Add

        public virtual T Add(T entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var result = await _dbSet.AddAsync(entity);
            return result.Entity;
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        #endregion

        #region Update

        public virtual T Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        #endregion

        #region Delete

        public virtual void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual void DeleteRange(Expression<Func<T, bool>> expression)
        {
            var entities = _dbSet.Where(expression).ToList();
            DeleteRange(entities);
        }

        #endregion

        #region Soft Delete

        public virtual async Task SoftDeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                await SoftDeleteAsync(entity);
            }
        }

        public virtual async Task SoftDeleteAsync(T entity)
        {
            if (entity is ISoftDeletable softDeletable)
            {
                softDeletable.IsDeleted = true;
                softDeletable.DeletedDate = DateTime.UtcNow;
                Update(entity);
            }
            else
            {
                Delete(entity);
            }
        }

        public virtual async Task SoftDeleteRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                await SoftDeleteAsync(entity);
            }
        }

        public virtual async Task RestoreAsync(int id)
        {
            var entity = await GetByIdWithDeletedAsync(id);
            if (entity != null)
            {
                await RestoreAsync(entity);
            }
        }

        public virtual async Task RestoreAsync(T entity)
        {
            if (entity is ISoftDeletable softDeletable)
            {
                softDeletable.IsDeleted = false;
                softDeletable.DeletedDate = null;
                Update(entity);
            }
        }

        #endregion

        #region Bulk Operations

        public virtual async Task<int> BulkUpdateAsync(Expression<Func<T, bool>> filter, Expression<Func<T, T>> updateExpression)
        {
            // Note: Bu method için EF Core Extensions (EFCore.BulkExtensions) kullanılabilir
            // Şimdilik basit implementation
            var entities = await _dbSet.Where(filter).ToListAsync();

            foreach (var entity in entities)
            {
                var updated = updateExpression.Compile()(entity);
                _context.Entry(entity).CurrentValues.SetValues(updated);
            }

            return entities.Count;
        }

        public virtual async Task<int> BulkDeleteAsync(Expression<Func<T, bool>> filter)
        {
            var entities = await _dbSet.Where(filter).ToListAsync();
            _dbSet.RemoveRange(entities);
            return entities.Count;
        }

        #endregion
    }
}
