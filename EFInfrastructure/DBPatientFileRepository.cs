using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DomainServices.Interfaces;
using EFInfrastructure;

namespace EFInfrastructure
{
    class DBPatientFileRepository : IPatientFileRepository
    {
        private readonly FysioDbContext _context;
        public DBPatientFileRepository(FysioDbContext context)
        {
            _context = context;
        }

        public void AddPatientFile(PatientFile patientFile)
        {
            _context.Add(patientFile);
            _context.SaveChanges();
        }

        public List<PatientFile> GetPatientFilesForPatient(Patient patient)
        {
            return _context.PatientFiles.Where(p => p.Patient == patient).ToList();
        }

        public void UpdatePatientFile(int id, PatientFile updatePatientFile)
        {
            PatientFile old = _context.PatientFiles.Where(p => p.Id == id).FirstOrDefault();
            old = updatePatientFile;
            _context.SaveChanges();            
        }
    }
}
