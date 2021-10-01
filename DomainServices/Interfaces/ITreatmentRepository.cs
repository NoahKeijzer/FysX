using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DomainServices.Interfaces
{
    public interface ITreatmentRepository
    {
        public void AddTreatment(Treatment treatment);
        public void UpdateTreatment(int id, Treatment updatedTreatment);
        public Treatment GetTreatmentById(int id);
        public List<Treatment> GetTreatmentsForPatient(Patient patient);
        public List<Treatment> GetTreatmentsForTreator(Treator treator);
        public List<Treatment> GetTreatmentsForTreatorToday(Treator treator);
        public List<Treatment> GetUpcomingTreatmentsForTreator(Treator treator);
    }
}
