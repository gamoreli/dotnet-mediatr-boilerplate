using System;
using System.Linq.Expressions;

namespace MediatorBoilerplate.Domain.Core.Base.Queries
{
    /// <summary>
    /// Creates a query for a paginated result using AutoMapper to Project
    /// </summary>
    /// <typeparam name="TModel">Entity Framework Model (predicate)</typeparam>
    public class AutoMapperPaginatedQuery<TModel> : AutoMapperQuery<TModel> where TModel : class
    {
        private const int PageSizeMin = 10;
        private const int PageMin = 1;

        public AutoMapperPaginatedQuery(int pageSize = PageSizeMin,
            int pageSkip = PageMin,
            Expression<Func<TModel, bool>> predicate = null)
            : base(predicate)
        {
            PageSize = pageSize;
            Page = pageSkip * PageSize;
        }

        public int PageSize { get; }
        public int Page { get; }
    }

    /// <summary>
    /// Creates a query for a paginated ordered result using AutoMapper to Project
    /// </summary>
    /// <typeparam name="TModel">Entity Framework Model (predicate)</typeparam>
    /// <typeparam name="TProjection">Projection Result</typeparam>
    /// <typeparam name="TKey">OrderBy Key Type</typeparam>
    public class AutoMapperPaginatedOrderedQuery<TModel, TProjection, TKey> : AutoMapperOrderedQuery<TModel, TProjection, TKey> where TModel : class
    {
        private const int PageSizeMin = 10;
        private const int PageMin = 1;

        public AutoMapperPaginatedOrderedQuery(Expression<Func<TProjection, TKey>> orderBy,
            Expression<Func<TModel, bool>> predicate = null, int pageSize = PageSizeMin, int pageSkip = PageMin)
            : base(predicate: predicate, orderBy: orderBy)
        {
            PageSize = pageSize;
            Page = pageSkip * PageSize;
        }

        public int PageSize { get; }
        public int Page { get; }
    }

    /// <summary>
    /// Creates a query for an ordered result using AutoMapper to Project
    /// </summary>
    /// <typeparam name="TModel">Entity Framework Model (predicate)</typeparam>
    /// <typeparam name="TProjection">Projection Result</typeparam>
    /// <typeparam name="TKey">OrderBy Key Type</typeparam>
    public class AutoMapperOrderedQuery<TModel, TProjection, TKey> : AutoMapperQuery<TModel> where TModel : class
    {
        public AutoMapperOrderedQuery(Expression<Func<TProjection, TKey>> orderBy,
            Expression<Func<TModel, bool>> predicate = null)
            : base(predicate) => OrderBy = orderBy;

        public Expression<Func<TProjection, TKey>> OrderBy { get; }
    }

    /// <summary>
    /// Creates a query for a result using AutoMapper to Project
    /// </summary>
    /// <typeparam name="TModel">Entity Framework Model (predicate)</typeparam>
    public class AutoMapperQuery<TModel> where TModel : class
    {
        public AutoMapperQuery(Expression<Func<TModel, bool>> predicate = null) => Predicate = predicate;

        public Expression<Func<TModel, bool>> Predicate { get; }
    }
}