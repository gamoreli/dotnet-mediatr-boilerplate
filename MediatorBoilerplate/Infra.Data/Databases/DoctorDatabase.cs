using System.Collections.Generic;
using System.Linq;
using MediatorBoilerplate.Domain.Models;
using static MediatorBoilerplate.Infra.Data.Databases.HospitalDatabase;
using static MediatorBoilerplate.Infra.Data.Databases.OrganizationDatabase;

namespace MediatorBoilerplate.Infra.Data.Databases
{
    public static class DoctorDatabase
    {
        public static List<Doctor> Doctors => new()
        {
            new Doctor("Doctor One", "CRM1", Hospitals.First().Id, Organizations.First().Id),
            new Doctor("Doctor Two", "CRM2", Hospitals.First().Id, Organizations.First().Id),
            new Doctor("Doctor Three", "CRM3", Hospitals.First().Id, Organizations.First().Id),
        };
        
        public static void Add(Doctor doctor) => Doctors.Add(doctor);
    }
}