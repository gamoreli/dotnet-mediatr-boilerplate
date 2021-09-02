using System;
using System.Collections.Generic;
using MediatorBoilerplate.Domain.Core.Base.Responses;
using MediatorBoilerplate.Domain.Core.Pipeline.Logging;
using MediatorBoilerplate.Domain.Core.Pipeline.Validation;
using MediatorBoilerplate.Domain.Core.Pipeline.VerifyHospital;
using MediatorBoilerplate.Domain.Core.Pipeline.VerifyOrganization;

namespace MediatorBoilerplate.Domain.Features.Doctor.AddDoctor
{
    public record AddDoctorMessage(string Name, string Crm, Guid HospitalId, Guid OrganizationId)
        : ILoggingBehavior<Response>,
        IMessageValidationBehavior<Response>,
        IVerifyOrganizationBehavior<Response>,
        IVerifyHospitalBehavior<Response>
    {
        public IEnumerable<string> InputErrors()
        {
            if (string.IsNullOrEmpty(Name)) yield return "Doctor name is required";
            if (string.IsNullOrEmpty(Crm)) yield return "Doctor crm is required";        
        }
    }
}