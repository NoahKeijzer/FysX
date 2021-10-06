using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Infra
{
    public interface ITreatmentRepository
    {
        public TreatmentType GetTreatmentById(string value);
        public IEnumerable<TreatmentType> GetAllTreatments();
        public void AddTreatment(TreatmentType t);
    }
}
