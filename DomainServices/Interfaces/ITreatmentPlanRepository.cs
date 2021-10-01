using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DomainServices.Interfaces
{
    public interface ITreatmentPlanRepository
    {
        public void AddTreatmentPlan(TreatmentPlan treatmentPlan);
        public void UpdateTreatmentPlan(int id, TreatmentPlan updatedTreatmentPlan);
        public TreatmentPlan GetTreatmentPlan(int id);
    }
}
