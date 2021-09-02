using System;
using System.Collections.Generic;
using MediatorBoilerplate.Domain.Core.Base.Responses;
using MediatorBoilerplate.Domain.Core.Pipeline.Logging;
using MediatorBoilerplate.Domain.Core.Pipeline.Validation;
using MediatorBoilerplate.Domain.Core.Pipeline.VerifyHospital;
using MediatorBoilerplate.Domain.Core.Pipeline.VerifyOrganization;

namespace MediatorBoilerplate.Domain.Features.Patient.AddPatient
{
    public record AddPatientMessage(string Name,
            int Age,
            string Address,
            Guid HospitalId,
            Guid OrganizationId)
        : ILoggingBehavior<Response>,
          IMessageValidationBehavior<Response>,
          IVerifyOrganizationBehavior<Response>,
          IVerifyHospitalBehavior<Response>
    {
        public IEnumerable<string> InputErrors()
        {
            if (string.IsNullOrEmpty(Name)) yield return "Patient name is required";
            if (string.IsNullOrEmpty(Address)) yield return "Patient address is required";
            if (Age < 16) yield return "Patient must be older than 16";
        }
    }
}