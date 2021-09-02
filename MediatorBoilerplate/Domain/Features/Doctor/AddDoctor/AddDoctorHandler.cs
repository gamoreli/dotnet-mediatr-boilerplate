using System.Threading;
using System.Threading.Tasks;
using MediatorBoilerplate.Domain.Core.Base.Responses;
using MediatorBoilerplate.Infra.Data.Databases;
using MediatR;

namespace MediatorBoilerplate.Domain.Features.Doctor.AddDoctor
{
    public class AddDoctorHandler : IRequestHandler<AddDoctorMessage, Response>
    {
        public Task<Response> Handle(AddDoctorMessage request, CancellationToken cancellationToken)
        {
            var (name, crm, hospitalId, organizationId) = request;
            
            DoctorDatabase.Add(new(name, crm, hospitalId, organizationId));
            
            return Task.FromResult(new Response());
        }
    }
}