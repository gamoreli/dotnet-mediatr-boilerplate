using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LanguageExt;
using MediatorBoilerplate.Domain.Core.Base.Failures;
using MediatorBoilerplate.Domain.Core.Base.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediatorBoilerplate.Domain.Core.Pipeline.Validation
{
    public class
        MessageValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, Either<Failure, Response>>
        where TRequest : IMessageValidationBehavior<Either<Failure, Response>>
    {
        private readonly ILogger _logger;
        private readonly IValidator<TRequest> _validator;

        public MessageValidationBehavior(ILogger logger, IValidator<TRequest> validator)
        {
            _logger = logger;
            _validator = validator;
        }

        public async Task<Either<Failure, Response>> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<Either<Failure, Response>> next)
        {
            // var failures = (await _validator.ValidateAsync(request, cancellationToken)).Errors.Where(failure => failure != null).ToList();
            //
            // if (failures.Any())
            // {
            //     return new BadRequestFailure(message: "Erro");
            // }

            return await next();
        }
    }
}