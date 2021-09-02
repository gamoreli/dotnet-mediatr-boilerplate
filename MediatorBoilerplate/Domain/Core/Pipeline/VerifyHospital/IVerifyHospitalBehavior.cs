using System;
using MediatR;

namespace MediatorBoilerplate.Domain.Core.Pipeline.VerifyHospital
{
    public interface IVerifyHospitalBehavior<out TResponse> : IRequest<TResponse>
    {
        public Guid HospitalId { get; init; }
        public Guid OrganizationId { get; init; }
    }
}