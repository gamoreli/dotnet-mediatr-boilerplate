using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using MediatorBoilerplate.Domain.Core.Base.Failures;
using MediatorBoilerplate.Domain.Core.Base.Responses;
using MediatR;

namespace MediatorBoilerplate.Domain.Features.User.Commands.CreateUser
{
    public class
        CreateUserHandler : IRequestHandler<CreateUserMessage, Either<BadRequestFailure, CommandResponse<Task>>>
    {
        public async Task<Either<BadRequestFailure, CommandResponse<Task>>> Handle(CreateUserMessage request,
            CancellationToken cancellationToken)
        {
            var (email, name) = request;

            return await Task.FromResult(new CommandResponse<Task>());
        }
    }
}