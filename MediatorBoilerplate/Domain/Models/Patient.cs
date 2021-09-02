using System;
using MediatorBoilerplate.Domain.Core.Base.Entities;

namespace MediatorBoilerplate.Domain.Models
{
    public record Patient(string Name, int Age, string Address, Guid HospitalId, Guid OrganizationId) : Entity<Guid>;
}