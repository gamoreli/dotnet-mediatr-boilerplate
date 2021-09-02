using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatorBoilerplate.Domain.Core.Pipeline.Validation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediatorBoilerplate.Domain.Core.Pipeline.Logging
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IMessageValidationBehavior<TResponse>
    {
        private readonly ILogger _logger;
        public LoggingBehavior(ILogger logger, IValidator<TRequest> validator) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation("Starting message execution");

            var response =  await next();
            
            _logger.LogInformation("Message executed");

            return response;
        }
    }
}