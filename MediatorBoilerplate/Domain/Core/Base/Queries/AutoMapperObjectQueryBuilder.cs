using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LanguageExt;
using MediatorBoilerplate.Domain.Core.Base.Failures;
using MediatorBoilerplate.Domain.Core.Base.Queries.Interfaces;
using MediatorBoilerplate.Domain.Core.Base.Responses;
using MediatorBoilerplate.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MediatorBoilerplate.Domain.Core.Base.Queries
{
    public class AutoMapperObjectQueryBuilder : IAutoMapperListQueryBuilder
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Entity Framework Context</param>
        /// <param name="mapper">AutoMapper</param>
        public AutoMapperObjectQueryBuilder(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns an unique entity of TProjection (Basic Query)
        /// </summary>
        /// <param name="query">
        /// AutoMapperQuery(predicate = null)
        /// </param>
        /// <typeparam name="TModel">Entity Framework Model</typeparam>
        /// <typeparam name="TProjection">Response Type</typeparam>
        /// <returns>If return is not null returns QueryResponse of TProjection, otherwise returns NotFoundFailure</returns>
        public async Task<Either<NotFoundFailure, QueryResponse<TProjection>>> Return<TModel, TProjection>(AutoMapperQuery<TModel> query)
            where TModel : class
        {
            var entity = await GetQueryable<TModel, TProjection>(query).FirstOrDefaultAsync();

            if (entity is not null)
                return new QueryResponse<TProjection>(entity);

            return new NotFoundFailure();
        }

        /// <summary>
        /// Returns an enumerable of TProjection (Basic Query)
        /// </summary>
        /// <param name="query">
        /// AutoMapperQuery(predicate = null)
        /// </param>
        /// <typeparam name="TModel">Entity Framework Model</typeparam>
        /// <typeparam name="TProjection">Response Type</typeparam>
        /// <returns>If return is not null returns QueryResponse of TProjection, otherwise returns NotFoundFailure</returns>
        async Task<Either<NotFoundFailure, QueryResponse<IEnumerable<TProjection>>>> IAutoMapperListQueryBuilder.Return<TModel, TProjection>(AutoMapperQuery<TModel> query)
        {
            var entities = await GetQueryable<TModel, TProjection>(query).ToListAsync();

            if (entities.Any())
                return new QueryResponse<IEnumerable<TProjection>>(entities);

            return new NotFoundFailure();
        }

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
        public async Task<Either<NotFoundFailure, QueryResponse<IEnumerable<TProjection>>>> Return<TModel, TProjection, TKey>(AutoMapperOrderedQuery<TModel, TProjection, TKey> query)
            where TModel : class
        {
            var entities = await GetQueryable<TModel, TProjection>(query)
                .OrderBy(query.OrderBy)
                .ToListAsync();

            if (entities.Any())
                return new QueryResponse<IEnumerable<TProjection>>(entities);

            return new NotFoundFailure();
        }

        /// <summary>
        /// Returns a paginated enumerable of TProjection
        /// </summary>
        /// <param name="query">
        /// AutoMapperPaginatedQuery(pageSize, page, predicate = null)
        /// </param>
        /// <typeparam name="TModel">Entity Framework Model</typeparam>
        /// <typeparam name="TProjection">Response Type</typeparam>
        /// <returns>If return is not null returns QueryResponsePaginated of TProjection, otherwise returns NotFoundFailure</returns>
        public async Task<Either<NotFoundFailure, QueryResponsePaginated<TProjection>>> Return<TModel, TProjection>(AutoMapperPaginatedQuery<TModel> query)
            where TModel : class
        {
            var entities = await GetQueryable<TModel, TProjection>(query)
                .Skip(query.Page)
                .Take(query.PageSize)
                .ToListAsync();

            if (entities.Any())
                return new QueryResponsePaginated<TProjection>(entities, query.Page,
                    await GetQueryable<TModel, TProjection>(query).CountAsync());

            return new NotFoundFailure();
        }

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
        public async Task<Either<NotFoundFailure, QueryResponsePaginated<TProjection>>> Return<TModel, TProjection, TKey>(AutoMapperPaginatedOrderedQuery<TModel, TProjection, TKey> query)
            where TModel : class
        {
            var queryable = GetQueryable<TModel, TProjection>(query)
                .Skip(query.Page)
                .Take(query.PageSize);

            if (query.OrderBy is not null)
                queryable = queryable.OrderBy(query.OrderBy);

            var entities = await queryable.ToListAsync();

            if (entities.Any())
                return new QueryResponsePaginated<TProjection>(entities, query.Page,
                    await GetQueryable<TModel, TProjection>(query).CountAsync());

            return new NotFoundFailure();
        }

        /// <summary>
        /// Create the core Entity Framework Query projecting with AutoMapper
        /// </summary>
        /// <param name="query">AutoMapperQuery(predicate = null)</param>
        /// <typeparam name="TModel">Entity Framework Model</typeparam>
        /// <typeparam name="TProjection">Response Type</typeparam>
        /// <returns>IQueryable of TProjection</returns>
        private IQueryable<TProjection> GetQueryable<TModel, TProjection>(AutoMapperQuery<TModel> query)
            where TModel : class
        {
            IQueryable<TModel> model = _context.Set<TModel>();

            if (query.Predicate is not null)
                model = model.Where(query.Predicate);

            return model.ProjectTo<TProjection>(_mapper.ConfigurationProvider);
        }
    }
}