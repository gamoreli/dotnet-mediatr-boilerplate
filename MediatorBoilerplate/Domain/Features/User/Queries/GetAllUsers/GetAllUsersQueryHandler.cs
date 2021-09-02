using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using MediatorBoilerplate.Domain.Core.Base.Failures;
using MediatorBoilerplate.Domain.Core.Base.Queries;
using MediatorBoilerplate.Domain.Core.Base.Queries.Interfaces;
using MediatorBoilerplate.Domain.Core.Base.Responses;
using MediatorBoilerplate.Domain.Features.User.Queries.Shared;
using MediatR;

namespace MediatorBoilerplate.Domain.Features.User.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery,
        Either<NotFoundFailure, QueryResponsePaginated<UserProjection>>>
    {
        private readonly IAutoMapperListQueryBuilder _autoMapperListQueryBuilder;

        public GetAllUsersQueryHandler(IAutoMapperListQueryBuilder autoMapperListQueryBuilder) =>
            _autoMapperListQueryBuilder = autoMapperListQueryBuilder;

        public Task<Either<NotFoundFailure, QueryResponsePaginated<UserProjection>>> Handle(
            GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return _autoMapperListQueryBuilder.Return<Models.User, UserProjection>(
                new AutoMapperPaginatedQuery<Models.User>());
        }
    }
}