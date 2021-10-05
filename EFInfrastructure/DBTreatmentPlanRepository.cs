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
    public class DBTreatmentPlanRepository : ITreatmentPlanRepository
    {
        private readonly FysioDbContext _context;
        public DBTreatmentPlanRepository(FysioDbContext context)
        {
            _context = context;
        }

        public void AddTreatmentPlan(TreatmentPlan treatmentPlan)
        {
            _context.Add(treatmentPlan);
            _context.SaveChanges();
        }

        public TreatmentPlan GetTreatmentPlan(int id)
        {
            return _context.TreatmentPlans.Where(p => p.Id == id).FirstOrDefault();
        }

        public void UpdateTreatmentPlan(int id, TreatmentPlan updatedTreatmentPlan)
        {
            TreatmentPlan old = _context.TreatmentPlans.Where(p => p.Id == id).FirstOrDefault();
            old = updatedTreatmentPlan;
            _context.SaveChanges();
        }
    }
}
