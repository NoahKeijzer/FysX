using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Infra
{
    public class DBTreatmentRepository : ITreatmentRepository
    {
        private readonly ApiDbContext context;

        public DBTreatmentRepository(ApiDbContext context)
        {
            this.context = context;
        }

        public void AddTreatment(TreatmentType t)
        {
            context.Add(t);
            context.SaveChanges();
        }

        public IQueryable<TreatmentType> GetAll()
        {
            return context.TreatmentTypes.AsQueryable();
        }

        public IEnumerable<TreatmentType> GetAllTreatments()
        {
            return context.TreatmentTypes.ToList();
        }

        public TreatmentType GetTreatmentById(string value)
        {
            return context.TreatmentTypes.Where(p => p.Value.Equals(value)).FirstOrDefault();
        }
    }
}
