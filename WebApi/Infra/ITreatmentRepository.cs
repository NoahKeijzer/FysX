using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Infra
{
    public interface ITreatmentRepository
    {
        public TreatmentType GetTreatmentById(int value);
        public IEnumerable<TreatmentType> GetAllTreatments();
    }
}
