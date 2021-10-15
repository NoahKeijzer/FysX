using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using DomainServices;

namespace ApiInfrastructure
{
    public class DBDiagnosisRepository : DomainServices.IDiagnosisRepository
    {
        private readonly ApiDbContext context;

        public DBDiagnosisRepository(ApiDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Diagnosis> GetAllDiagnoses()
        {
            return context.Diagnoses.ToList();
        }

        public IEnumerable<Diagnosis> GetDiagnosesByCategory(string category)
        {
            return context.Diagnoses.Where(p => p.BodyPart == category).ToList();
        }

        public Diagnosis GetDiagnosisById(int value)
        {
            return context.Diagnoses.Where(p => p.Value.Equals(value)).FirstOrDefault();
        }

        public void AddDiagnosis(Diagnosis diagnosis)
        {
            context.Diagnoses.Add(diagnosis);
            context.SaveChanges();

        }

        public IEnumerable<string> GetCategories()
        {
            List<string> categories = context.Diagnoses.GroupBy(p => p.BodyPart).Select(p => p.Key).ToList();
            return categories;
        }


        public IQueryable<Diagnosis> GetAll()
        {
            return context.Diagnoses.AsQueryable();
        }

        public Diagnosis GetById(int id)
        {
            return context.Diagnoses.Where(p => p.Id == id).First();
        }
    }
}
