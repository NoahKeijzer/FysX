using System.Collections.Generic;
using System.Linq;
using Domain;

namespace DomainServices.Interfaces
{
    public interface IDiagnosisRepository
    {
        public IEnumerable<Diagnosis> GetAllDiagnoses(string token);
        public IEnumerable<Diagnosis> GetDiagnosesByCategory(string category, string token);
        public Diagnosis GetDiagnosisById(int id, string token);
        public IEnumerable<string> GetCategories(string token);
    }
}