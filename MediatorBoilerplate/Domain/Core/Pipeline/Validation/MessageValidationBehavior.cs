using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatorBoilerplate.Domain.Core.Base.Exceptions;
using MediatorBoilerplate.Domain.Core.Base.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediatorBoilerplate.Domain.Core.Pipeline.Validation
{
    public class MessageValidationBehavior<TRequest> : IPipelineBehavior<TRequest, Response>
        where TRequest : IMessageValidationBehavior<Response>
    {
        private readonly ILogger _logger;
        
        public MessageValidationBehavior(ILogger logger) => _logger = logger;

        public async Task<Response> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<Response> next)
        {
            _logger.LogInformation("Starting input validation");

            if (!request.InputErrors().Any())
                throw new CustomValidationException(request.InputErrors()
                    .Aggregate((a, b) => $"{a}, {b}"), "Input");

            _logger.LogInformation("Input validation finished");

            return await next();
        }
    }
}