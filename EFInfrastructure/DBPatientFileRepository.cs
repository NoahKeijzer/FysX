using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DomainServices.Interfaces;
using EFInfrastructure;
using Microsoft.EntityFrameworkCore;

namespace EFInfrastructure
{
    public class DBPatientFileRepository : IPatientFileRepository
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

        public PatientFile GetCurrentPatientFileForPatient(Patient patient)
        {
            return _context.PatientFiles.Include(p => p.Treatments).Include(p => p.Intaker).Include(p => p.Patient).Include(p => p.MainTreator).Include(p => p.Comments).Include(p => p.TreatmentPlan).Where(p => p.Patient == patient && p.EndDate == DateTime.MinValue).FirstOrDefault();
        }

        public async void UpdatePatientFile(PatientFile updatePatientFile)
        {
            _context.SaveChanges();
        }

        public PatientFile GetPatientFileById(int id)
        {
            return _context.PatientFiles.Include(p => p.Treatments).Include(p => p.Intaker).Include(p => p.Patient).Include(p => p.MainTreator).Include(p => p.Comments).Include(p => p.TreatmentPlan).Where(p => p.Id == id).FirstOrDefault();
        }
    }
}
