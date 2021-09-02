using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;

namespace MediatorBoilerplate.Infra.Data.Infra
{
    public static class ModelBuilderExtensions
    {
        public static void AddAllConfigurations(this ModelBuilder modelBuilder)
        {
            var types = typeof(EntityTypeConfiguration<>).GetTypeInfo().Assembly.GetTypes();
            var typesToRegister = types
                .Where(type => !string.IsNullOrEmpty(type.Namespace) &&
                               type.GetTypeInfo().BaseType != null &&
                               type.GetTypeInfo().ContainsGenericParameters == false &&
                               type.GetTypeInfo().BaseType!.GetTypeInfo().IsGenericType &&
                               type.GetTypeInfo().BaseType?.GetGenericTypeDefinition() ==
                               typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                AddConfiguration(modelBuilder, configurationInstance);
            }
        }

        private static void AddConfiguration<TEntity>(ModelBuilder modelBuilder,
            EntityTypeConfiguration<TEntity> configuration)
            where TEntity : class
        {
            configuration.Map(modelBuilder.Entity<TEntity>());
        }

        public static void AddQueryFilter<T>(this EntityTypeBuilder entityTypeBuilder,
            Expression<Func<T, bool>> expression)
        {
            var parameterType = Expression.Parameter(entityTypeBuilder.Metadata.ClrType);
            var expressionFilter = ReplacingExpressionVisitor.Replace(
                expression.Parameters.Single(), parameterType, expression.Body);

            var internalEntityTypeBuilder = entityTypeBuilder.GetInternalEntityTypeBuilder();
            if (internalEntityTypeBuilder.Metadata?.GetQueryFilter() != null)
            {
                var currentQueryFilter = internalEntityTypeBuilder.Metadata.GetQueryFilter();
                var currentExpressionFilter = ReplacingExpressionVisitor.Replace(
                    currentQueryFilter.Parameters.Single(), parameterType, currentQueryFilter.Body);
                expressionFilter = Expression.AndAlso(currentExpressionFilter, expressionFilter);
            }

            var lambdaExpression = Expression.Lambda(expressionFilter, parameterType);
            entityTypeBuilder.HasQueryFilter(lambdaExpression);
        }

        private static InternalEntityTypeBuilder GetInternalEntityTypeBuilder(this EntityTypeBuilder entityTypeBuilder)
        {
            var internalEntityTypeBuilder = typeof(EntityTypeBuilder)
                .GetProperty("Builder", BindingFlags.NonPublic | BindingFlags.Instance)?
                .GetValue(entityTypeBuilder) as InternalEntityTypeBuilder;

            return internalEntityTypeBuilder;
        }
    }

    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}