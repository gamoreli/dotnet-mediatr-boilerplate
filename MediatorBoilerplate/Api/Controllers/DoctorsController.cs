using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatorBoilerplate.Domain.Core.Base.Exceptions;
using MediatorBoilerplate.Domain.Features.Doctor.AddDoctor;
using MediatorBoilerplate.Domain.Models;
using MediatorBoilerplate.Infra.Data.Databases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MediatorBoilerplate.Api.Controllers
{
    public class DoctorsController: BaseController
    {
        private readonly ILogger<PatientController> _logger;

        public DoctorsController(ILogger<PatientController> logger, IMediator mediator) : base(mediator) => _logger = logger;

        [HttpPost("v1")]
        public IActionResult CreateDoctor([FromBody] Doctor doctor)
        {
            var errors = new List<string>();

            _logger.LogInformation("Starting input validation");

            if (string.IsNullOrEmpty(doctor.Name)) errors.Add("Doctor name is required");
            if (string.IsNullOrEmpty(doctor.Crm)) errors.Add("Doctor crm is required");

            _logger.LogInformation("Input validation finished");

            if (errors.Any())
                return new BadRequestObjectResult(new {errors, ValidationType = "Input"});

            _logger.LogInformation("Starting business validation");

            // verify org
            if (OrganizationDatabase.Organizations.All(a => a.Id != doctor.OrganizationId))
                errors.Add("Invalid Organization");

            // verify hospital
            if (OrganizationDatabase.Organizations.Any(a =>
                a.Id == doctor.OrganizationId && a.Hospitals.All(h => h.Id != doctor.HospitalId)))
                errors.Add("Hospital does not belong to the Organization");

            if (errors.Any())
                return new BadRequestObjectResult(new {errors, ValidationType = "Business"});

            _logger.LogInformation("Business validation finished");
            
            DoctorDatabase.Add(doctor);

            return new OkObjectResult(doctor);
        }
        
        [HttpPost("v2")]
        public async Task<IActionResult> CreateDoctor([FromBody] AddDoctorMessage doctor)
        {
            try
            {
                var response = await Mediator.Send(doctor);
                
                return new OkObjectResult(response);
            }
            catch (CustomValidationException e)
            {
                return new BadRequestObjectResult(new {e.Message, ValidationType = e.Type});
            }
        }
    }
}