using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using MediatorBoilerplate.Domain.Core.Base.Failures;
using MediatorBoilerplate.Domain.Core.Base.Queries;
using MediatorBoilerplate.Domain.Core.Base.Queries.Interfaces;
using MediatorBoilerplate.Domain.Core.Base.Responses;
using MediatorBoilerplate.Domain.Features.User.Queries.Shared;
using MediatR;

namespace MediatorBoilerplate.Domain.Features.User.Queries.GetUser
{
    public class
        GetUserQueryHandler : IRequestHandler<GetUserQuery, Either<NotFoundFailure, QueryResponse<UserProjection>>>
    {
        private readonly IAutoMapperObjectQueryBuilder _autoMapperObjectQueryHandler;

        public GetUserQueryHandler(IAutoMapperObjectQueryBuilder autoMapperObjectQueryHandler) =>
            _autoMapperObjectQueryHandler = autoMapperObjectQueryHandler;

        public Task<Either<NotFoundFailure, QueryResponse<UserProjection>>> Handle(GetUserQuery request,
            CancellationToken cancellationToken) =>
            _autoMapperObjectQueryHandler.Return<Models.User, UserProjection>(
                new AutoMapperQuery<Models.User>(user => user.Id == request.Id));
    }
}