using System;

namespace MediatorBoilerplate.Domain.Core.Base.Entities
{
    public abstract record Entity<TKey> : IAuditEntity, IIdentifier<TKey> where TKey : IEquatable<TKey>, new()
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public TKey Id { get; set; } = new();

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }
    }
}