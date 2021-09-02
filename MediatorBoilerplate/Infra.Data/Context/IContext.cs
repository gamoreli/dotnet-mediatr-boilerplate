using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace MediatorBoilerplate.Infra.Data.Context
{
    public interface IContext : IDisposable
    {
        bool HasActiveTransaction { get; }
        DatabaseFacade Database { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void SetModified(object entity);
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(IDbContextTransaction transaction);
        void RollbackTransaction();

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default);
    }
}