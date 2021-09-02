using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatorBoilerplate.Domain.Core.Base.Exceptions;
using MediatorBoilerplate.Domain.Core.Base.Responses;
using MediatorBoilerplate.Infra.Data.Databases;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediatorBoilerplate.Domain.Core.Pipeline.VerifyOrganization
{
    public class VerifyOrganizationBehavior<TRequest> : IPipelineBehavior<TRequest, Response>
        where TRequest : IVerifyOrganizationBehavior<Response>
    {
        private readonly ILogger _logger;

        public VerifyOrganizationBehavior(ILogger logger) => _logger = logger;

        public async Task<Response> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<Response> next)
        {
            _logger.LogInformation($"Starting business validation {typeof(VerifyOrganizationBehavior<TRequest>).Name}");

            if (OrganizationDatabase.Organizations.All(a => a.Id != request.OrganizationId))
                throw new CustomValidationException("Invalid Organization", "Business");

            _logger.LogInformation($"Business validation {typeof(VerifyOrganizationBehavior<TRequest>).Name} finished");

            return await next();
        }
    }
}