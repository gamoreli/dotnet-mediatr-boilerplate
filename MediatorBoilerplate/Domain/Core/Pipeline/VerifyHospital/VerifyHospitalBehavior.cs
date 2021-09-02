using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatorBoilerplate.Domain.Core.Base.Exceptions;
using MediatorBoilerplate.Domain.Core.Base.Responses;
using MediatorBoilerplate.Infra.Data.Databases;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediatorBoilerplate.Domain.Core.Pipeline.VerifyHospital
{
    public class VerifyHospitalBehavior<TRequest> : IPipelineBehavior<TRequest, Response>
        where TRequest : IVerifyHospitalBehavior<Response>
    {
        private readonly ILogger _logger;

        public VerifyHospitalBehavior(ILogger logger) => _logger = logger;
        
        public async Task<Response> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Response> next)
        {
            _logger.LogInformation($"Starting business validation {typeof(VerifyHospitalBehavior<TRequest>).Name}");
            
            if (OrganizationDatabase.Organizations.Any(a =>
                a.Id == request.OrganizationId && a.Hospitals.All(h => h.Id != request.HospitalId)))
                throw new CustomValidationException("Invalid Hospital", "Business");

            _logger.LogInformation($"Business validation {typeof(VerifyHospitalBehavior<TRequest>).Name} finished");

            return await next();
        }
    }
}