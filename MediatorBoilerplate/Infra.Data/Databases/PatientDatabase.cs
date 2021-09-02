using System.Collections.Generic;
using System.Linq;
using MediatorBoilerplate.Domain.Models;
using static MediatorBoilerplate.Infra.Data.Databases.HospitalDatabase;
using static MediatorBoilerplate.Infra.Data.Databases.OrganizationDatabase;

namespace MediatorBoilerplate.Infra.Data.Databases
{
    public static class PatientDatabase
    {
        public static List<Patient> Patients => new()
        {
            new Patient("Patient One", 17, "Address One", Hospitals.First().Id, Organizations.First().Id),
            new Patient("Patient Two", 18, "Address Two", Hospitals.First().Id, Organizations.First().Id),
            new Patient("Patient Three", 19, "Address Three", Hospitals.First().Id, Organizations.First().Id),
        };
        
        public static void Add(Patient patient) => Patients.Add(patient);
    }
}