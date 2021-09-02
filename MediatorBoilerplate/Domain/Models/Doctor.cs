using System;
using MediatorBoilerplate.Domain.Core.Base.Entities;

namespace MediatorBoilerplate.Domain.Models
{
    public record Doctor(string Name, string Crm, Guid HospitalId, Guid OrganizationId) : Entity<Guid>;
}