using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using MediatorBoilerplate.Domain.Core.Base.Failures;
using MediatorBoilerplate.Domain.Core.Base.Responses;

namespace MediatorBoilerplate.Domain.Core.Base.Queries.Interfaces
{
    public interface IAutoMapperListQueryBuilder : IAutoMapperObjectQueryBuilder
    {
        /// <summary>
        /// Returns an enumerable of TProjection (Basic Query)
        /// </summary>
        /// <param name="query">
        /// AutoMapperQuery(predicate = null)
        /// </param>
        /// <typeparam name="TModel">Entity Framework Model</typeparam>
        /// <typeparam name="TProjection">Response Type</typeparam>
        /// <returns>If return is not null returns QueryResponse of TProjection, otherwise returns NotFoundFailure</returns>
        new Task<Either<NotFoundFailure, QueryResponse<IEnumerable<TProjection>>>> Return<TModel, TProjection>(
            AutoMapperQuery<TModel> query) where TModel : class;

        /// <summary>
        /// Returns an ordered enumerable of TProjection
        /// </summary>
        /// <param name="query">
        /// AutoMapperOrderedQuery(orderBy, predicate = null)
        /// </param>
        /// <typeparam name="TModel">Entity Framework Model</typeparam>
        /// <typeparam name="TProjection">Response Type</typeparam>
        /// <typeparam name="TKey">OrderBy Key Type</typeparam>
        /// <returns>If return is not null returns QueryResponse of TProjection, otherwise returns NotFoundFailure</returns>
        Task<Either<NotFoundFailure, QueryResponse<IEnumerable<TProjection>>>> Return<TModel, TProjection, TKey>(
            AutoMapperOrderedQuery<TModel, TProjection, TKey> query) where TModel : class;

        /// <summary>
        /// Returns a paginated enumerable of TProjection
        /// </summary>
        /// <param name="query">
        /// AutoMapperPaginatedQuery(pageSize, page, predicate = null)
        /// </param>
        /// <typeparam name="TModel">Entity Framework Model</typeparam>
        /// <typeparam name="TProjection">Response Type</typeparam>
        /// <returns>If return is not null returns QueryResponsePaginated of TProjection, otherwise returns NotFoundFailure</returns>
        Task<Either<NotFoundFailure, QueryResponsePaginated<TProjection>>> Return<TModel, TProjection>(
            AutoMapperPaginatedQuery<TModel> query) where TModel : class;

        /// <summary>
        /// Returns a paginated ordered enumerable of TProjection
        /// </summary>
        /// <param name="query">
        /// AutoMapperPaginatedOrderedQuery(orderBy, pageSize, page, predicate = null)
        /// </param>
        /// <typeparam name="TModel">Entity Framework Model</typeparam>
        /// <typeparam name="TProjection">Response Type</typeparam>
        /// <typeparam name="TKey">OrderBy Key Type</typeparam>
        /// <returns>If return is not null returns QueryResponsePaginated of TProjection, otherwise returns NotFoundFailure</returns>
        Task<Either<NotFoundFailure, QueryResponsePaginated<TProjection>>> Return<TModel, TProjection,
            TKey>(
            AutoMapperPaginatedOrderedQuery<TModel, TProjection, TKey> query) where TModel : class;
    }
}