using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using DomainServices.Interfaces;
using EFInfrastructure;

namespace EFInfrastructure
{
    public class DBPatientRepository : IPatientRepository
    {
        private readonly FysioDbContext _context;
        public DBPatientRepository(FysioDbContext context)
        {
            _context = context;
        }
        public void AddPatient(Patient p)
        {
            _context.Add(p);
            _context.SaveChanges();
        }

        public int AmountOfPatients()
        {
            return _context.Patients.Count();
        }

        public void DeletePatient(Patient p)
        {
            _context.Patients.Remove(p);
            _context.SaveChanges();
        }

        public List<Patient> GetAllPatients()
        {
            return _context.Patients.ToList();
        }

        public Patient GetPatientById(int id)
        {
            return _context.Patients.Where(p => p.Id == id).FirstOrDefault();
        }

        public Patient UpdatePatient(int Id, Patient UpdatedPatient)
        {
            Patient old = _context.Patients.Where(p => p.Id == Id).FirstOrDefault();
            old = UpdatedPatient;
            _context.SaveChanges();
            return old;
        }
    }
}
