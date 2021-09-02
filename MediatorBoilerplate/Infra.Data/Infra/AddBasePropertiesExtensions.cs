using System.Linq;
using System.Reflection;
using MediatorBoilerplate.Domain.Core.Base.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatorBoilerplate.Infra.Data.Infra
{
    public static class AddBasePropertiesExtensions
    {
        private static readonly MethodInfo SetAuditingShadowPropertiesMethodInfo = typeof(AddBasePropertiesExtensions)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetAuditingShadowProperties");

        public static void AddBaseProperties(this ModelBuilder modelBuilder)
        {
            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                var entityType = type.ClrType;

                if (typeof(IAuditEntity).IsAssignableFrom(entityType))
                {
                    var method = SetAuditingShadowPropertiesMethodInfo.MakeGenericMethod(entityType);
                    method.Invoke(modelBuilder, new object[] {modelBuilder});
                }
            }
        }

        public static void SetAuditingShadowProperties<T>(ModelBuilder builder) where T : class, IAuditEntity
        {
            builder.Entity<T>().Property(x => x.CreatedOn).IsRequired();
            builder.Entity<T>().Property(x => x.UpdatedOn).IsRequired();
        }
    }
}