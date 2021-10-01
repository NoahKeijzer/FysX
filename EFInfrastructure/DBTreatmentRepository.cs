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
    class DBTreatmentRepository : ITreatmentRepository
    {
        private readonly FysioDbContext _context;
        public DBTreatmentRepository(FysioDbContext context)
        {
            _context = context;
        }

        public void AddTreatment(Treatment treatment)
        {
            _context.Add(treatment);
            _context.SaveChanges();
        }

        public Treatment GetTreatmentById(int id)
        {
            return _context.Treatments.Where(p => p.Id == id).FirstOrDefault();
        }

        public List<Treatment> GetTreatmentsForPatient(Patient patient)
        {
            return _context.Treatments.Where(p => p.Patient == patient).ToList();
        }

        public List<Treatment> GetTreatmentsForTreator(Treator treator)
        {
            return _context.Treatments.Where(p => p.Treator == treator).ToList();
        }

        public List<Treatment> GetTreatmentsForTreatorToday(Treator treator)
        {
            return _context.Treatments.Where(p => p.Treator == treator && p.TreatmentDateTime.Date == DateTime.Now.Date).ToList();
        }

        public List<Treatment> GetUpcomingTreatmentsForTreator(Treator treator)
        {
            return _context.Treatments.Where(p => p.Treator == treator && p.TreatmentDateTime.Date > DateTime.Now.Date).ToList();
        }

        public void UpdateTreatment(int id, Treatment updatedTreatment)
        {
            Treatment old = _context.Treatments.Where(p => p.Id == id).FirstOrDefault();
            old = updatedTreatment;
            _context.SaveChanges();
        }
    }
}
