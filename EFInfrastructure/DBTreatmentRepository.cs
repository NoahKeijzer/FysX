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
    public class DBTreatmentRepository : ITreatmentRepository
    {
        private readonly FysioDbContext _context;
        public DBTreatmentRepository(FysioDbContext context)
        {
            _context = context;
        }

        public void AddTreatment(Treatment treatment)
        {
            //_context.Add(treatment);
            _context.Treatments.Add(treatment);
            _context.SaveChanges();
        }

        public bool DeleteTreatment(Treatment treatment)
        {
            try
            {
                _context.Treatments.Remove(treatment);
                _context.SaveChanges();
                return true;
            } catch( Exception e)
            {
                return false;
            }
        }

        public Treatment GetTreatmentById(int id)
        {
            return _context.Treatments.Where(p => p.Id == id).Include(p => p.Patient).Include(p => p.Treator).FirstOrDefault();
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
            if(old.TreatmentDateTime > DateTime.Now.AddDays(-1))
            {
                old.Type = updatedTreatment.Type;
                old.Location = updatedTreatment.Location;
                old.Particularities = updatedTreatment.Particularities;
                old.Description = updatedTreatment.Description;
                _context.SaveChanges();
            } else
            {
                throw new Exception("InvalidTreatmentEdit");
            }
        }
    }
}
