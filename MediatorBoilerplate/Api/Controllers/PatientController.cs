using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatorBoilerplate.Domain.Core.Base.Exceptions;
using MediatorBoilerplate.Domain.Features.Patient.AddPatient;
using MediatorBoilerplate.Domain.Models;
using MediatorBoilerplate.Infra.Data.Databases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MediatorBoilerplate.Api.Controllers
{
    public class PatientController : BaseController
    {
        private readonly ILogger<PatientController> _logger;

        public PatientController(ILogger<PatientController> logger, IMediator mediator) : base(mediator) => _logger = logger;

        [HttpPost("v1")]
        public IActionResult CreatePatient([FromBody] Patient patient)
        {
            var errors = new List<string>();

            _logger.LogInformation("Starting input validation");

            if (string.IsNullOrEmpty(patient.Name)) errors.Add("Patient name is required");
            if (string.IsNullOrEmpty(patient.Address)) errors.Add("Patient address is required");
            if (patient.Age < 16) errors.Add("Patient must be older than 16");

            _logger.LogInformation("Input validation finished");

            if (errors.Any())
                return new BadRequestObjectResult(new {errors, ValidationType = "Input"});

            _logger.LogInformation("Starting business validation");

            // verify org
            if (OrganizationDatabase.Organizations.All(a => a.Id != patient.OrganizationId))
                errors.Add("Invalid Organization");

            // verify hospital
            if (OrganizationDatabase.Organizations.Any(a =>
                a.Id == patient.OrganizationId && a.Hospitals.All(h => h.Id != patient.HospitalId)))
                errors.Add("Hospital does not belong to the Organization");

            if (errors.Any())
                return new BadRequestObjectResult(new {errors, ValidationType = "Business"});

            _logger.LogInformation("Business validation finished");
            
            PatientDatabase.Add(patient);

            return new OkObjectResult(patient);
        }
        
        [HttpPost("v2")]
        public async Task<IActionResult> CreatePatient([FromBody] AddPatientMessage patient)
        {
            try
            {
                var response = await Mediator.Send(patient);
                
                return new OkObjectResult(response);
            }
            catch (CustomValidationException e)
            {
                return new BadRequestObjectResult(new {e.Message, ValidationType = e.Type});
            }
        }
    }
}