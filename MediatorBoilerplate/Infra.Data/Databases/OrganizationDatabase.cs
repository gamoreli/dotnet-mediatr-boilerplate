using System.Collections.Generic;
using MediatorBoilerplate.Domain.Models;

namespace MediatorBoilerplate.Infra.Data.Databases
{
    public static class OrganizationDatabase
    {
        public static List<Organization> Organizations => new()
        {
            new Organization("Organization One"),
            new Organization("Organization Two"),
            new Organization("Organization Three"),
        };

        public static void Add(Organization organization) => Organizations.Add(organization);
    }
}