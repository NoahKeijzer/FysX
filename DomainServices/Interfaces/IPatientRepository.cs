using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace DomainServices.Interfaces
{
    public interface IPatientRepository
    {
        public List<Patient> GetAllPatients();

        public Patient GetPatientById(int id);

        public Patient GetPatientByEmail(string email);

        public void AddPatient(Patient p);

        public void DeletePatient(Patient p);

        public Patient UpdatePatient(int Id, Patient UpdatedPatient);

        public int AmountOfPatients();
    }
}
