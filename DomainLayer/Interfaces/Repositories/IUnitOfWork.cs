using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        // Repository Properties
        IGameRepository Games { get; }
        IUserRepository Users { get; }
        ICommentRepository Comments { get; }
        // Diğer repository'ler buraya eklenecek

        // Transaction Management
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        void Rollback();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

        // Bulk Operations
        Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters);
        Task<int> ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken, params object[] parameters);
    }
}
