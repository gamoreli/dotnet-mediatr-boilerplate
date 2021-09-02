using System.Threading.Tasks;
using LanguageExt;
using MediatorBoilerplate.Domain.Core.Base.Failures;
using MediatorBoilerplate.Domain.Core.Base.Responses;

namespace MediatorBoilerplate.Domain.Core.Base.Queries.Interfaces
{
    public interface IAutoMapperObjectQueryBuilder
    {
        /// <summary>
        /// Returns an unique entity of TProjection (Basic Query)
        /// </summary>
        /// <param name="query">
        /// AutoMapperQuery(predicate = null)
        /// </param>
        /// <typeparam name="TModel">Entity Framework Model</typeparam>
        /// <typeparam name="TProjection">Response Type</typeparam>
        /// <returns>If return is not null returns QueryResponse of TProjection, otherwise returns NotFoundFailure</returns>
        Task<Either<NotFoundFailure, QueryResponse<TProjection>>> Return<TModel, TProjection>(
            AutoMapperQuery<TModel> query) where TModel : class;
    }
}