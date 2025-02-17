﻿using System.Collections.Generic;
using System.Linq;
using Domain;

namespace DomainServices
{
    public interface IDiagnosisRepository
    {
        public IEnumerable<Diagnosis> GetAllDiagnoses();
        public IEnumerable<Diagnosis> GetDiagnosesByCategory(string category);
        public Diagnosis GetDiagnosisById(int id);
        public void AddDiagnosis(Diagnosis diagnosis);
        public IEnumerable<string> GetCategories();
        public IQueryable<Diagnosis> GetAll();
    }
}