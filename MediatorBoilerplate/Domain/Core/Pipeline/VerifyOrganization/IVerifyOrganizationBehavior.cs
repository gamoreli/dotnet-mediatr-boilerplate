using System;
using MediatR;

namespace MediatorBoilerplate.Domain.Core.Pipeline.VerifyOrganization
{
    public interface IVerifyOrganizationBehavior<out TResponse> : IRequest<TResponse>
    {
        public Guid OrganizationId { get; init; }
    }
}