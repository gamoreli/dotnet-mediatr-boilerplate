using System;
using LanguageExt;
using MediatorBoilerplate.Domain.Core.Base.Failures;
using MediatorBoilerplate.Domain.Core.Base.Responses;
using MediatorBoilerplate.Domain.Features.User.Queries.Shared;
using MediatR;

namespace MediatorBoilerplate.Domain.Features.User.Queries.GetUser
{
    public record GetUserQuery(Guid Id) : IRequest<Either<NotFoundFailure, QueryResponse<UserProjection>>>;
}