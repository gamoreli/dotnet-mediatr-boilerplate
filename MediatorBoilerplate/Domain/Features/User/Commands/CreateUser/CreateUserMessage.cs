using System.Threading.Tasks;
using LanguageExt;
using MediatorBoilerplate.Domain.Core.Base.Failures;
using MediatorBoilerplate.Domain.Core.Base.Responses;
using MediatorBoilerplate.Domain.Core.Pipeline.Logging;
using MediatorBoilerplate.Domain.Core.Pipeline.Transaction;
using MediatorBoilerplate.Domain.Core.Pipeline.Validation;

namespace MediatorBoilerplate.Domain.Features.User.Commands.CreateUser
{
    public record CreateUserMessage(string Email, string Name) : 
        IMessageValidationBehavior<Either<BadRequestFailure, CommandResponse<Task>>>,
            ITransactionBehavior, ILoggingBehavior;
}