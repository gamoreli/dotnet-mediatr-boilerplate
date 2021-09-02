using System;
using MediatorBoilerplate.Domain.Core.Base.Entities;

namespace MediatorBoilerplate.Domain.Models
{
    public record Hospital(string Name, string Address) : Entity<Guid>;
}