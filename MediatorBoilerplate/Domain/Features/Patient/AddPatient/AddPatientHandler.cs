using System.Threading;
using System.Threading.Tasks;
using MediatorBoilerplate.Domain.Core.Base.Responses;
using MediatorBoilerplate.Infra.Data.Databases;
using MediatR;

namespace MediatorBoilerplate.Domain.Features.Patient.AddPatient
{
    public class AddPatientHandler : IRequestHandler<AddPatientMessage, Response>
    {
        public Task<Response> Handle(AddPatientMessage request, CancellationToken cancellationToken)
        {
            var patient = new Models.Patient(
                request.Name, 
                request.Age, 
                request.Address, 
                request.HospitalId,
                request.OrganizationId);
            
            PatientDatabase.Add(patient);

            return Task.FromResult(new Response());
        }
    }
}