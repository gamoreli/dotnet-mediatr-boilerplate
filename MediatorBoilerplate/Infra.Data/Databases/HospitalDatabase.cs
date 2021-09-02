using System.Collections.Generic;
using MediatorBoilerplate.Domain.Models;

namespace MediatorBoilerplate.Infra.Data.Databases
{
    public static class HospitalDatabase
    {
        public static List<Hospital> Hospitals => new()
        {
            new Hospital("Organization One", "Address One"),
            new Hospital("Organization Two", "Address Two"),
            new Hospital("Organization Three", "Address Three"),
        };

        public static void Add(Hospital hospital) => Hospitals.Add(hospital);
    }
}