using LanguageExt;
using MediatorBoilerplate.Domain.Core.Base.Failures;
using MediatorBoilerplate.Domain.Core.Base.Responses;
using MediatorBoilerplate.Domain.Features.User.Queries.Shared;
using MediatR;

namespace MediatorBoilerplate.Domain.Features.User.Queries.GetAllUsers
{
    public record GetAllUsersQuery : IRequest<Either<NotFoundFailure, QueryResponsePaginated<UserProjection>>>;
}