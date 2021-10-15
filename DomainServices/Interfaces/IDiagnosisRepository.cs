using System.Collections.Generic;
using System.Linq;
using Domain;

namespace DomainServices.Interfaces
{
    public interface IDiagnosisRepository
    {
        public IEnumerable<Diagnosis> GetAllDiagnoses();
        public IEnumerable<Diagnosis> GetDiagnosesByCategory(string category);
        public Diagnosis GetDiagnosisById(int id);
        public IEnumerable<string> GetCategories();
    }
}