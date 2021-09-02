using System;
using MediatorBoilerplate.Domain.Core.Base.Entities;

namespace MediatorBoilerplate.Domain.Models
{
    public record User(string Name, string Email) : Entity<Guid>;
}