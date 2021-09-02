using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatorBoilerplate.Domain.Core.Base.Entities;
using MediatorBoilerplate.Infra.Data.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MediatorBoilerplate.Infra.Data.Context
{
    public class BoilerplateContext : DbContext, IContext
    {
        private IDbContextTransaction _currentTransaction;

        public BoilerplateContext(DbContextOptions<BoilerplateContext> options)
            : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            var timestamp = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries())
                if (entry.Entity is IAuditEntity)
                    if (entry.State is EntityState.Added or EntityState.Modified)
                    {
                        entry.Property("UpdatedOn").CurrentValue = timestamp;

                        if (entry.State == EntityState.Added) entry.Property("CreatedOn").CurrentValue = timestamp;
                    }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public bool HasActiveTransaction => _currentTransaction != null;

        public void SetModified(object entity) => Entry(entity).State = EntityState.Modified;

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null)
                return null;

            _currentTransaction = await Database.BeginTransactionAsync();

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction is null)
                throw new ArgumentNullException(nameof(transaction));

            if (transaction != _currentTransaction)
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string)))
                property.SetMaxLength(128);

            modelBuilder.AddBaseProperties();
            modelBuilder.AddAllConfigurations();

            base.OnModelCreating(modelBuilder);
        }
    }
}