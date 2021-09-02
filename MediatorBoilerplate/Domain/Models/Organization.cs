using System;
using System.Collections.Generic;
using MediatorBoilerplate.Domain.Core.Base.Entities;

namespace MediatorBoilerplate.Domain.Models
{
    public record Organization(string Name) : Entity<Guid>
    {
        public ICollection<Hospital> Hospitals { get; set; }
    }
}