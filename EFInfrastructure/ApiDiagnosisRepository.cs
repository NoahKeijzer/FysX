using Domain;
using DomainServices.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EFInfrastructure
{
    public class ApiDiagnosisRepository : IDiagnosisRepository
    {
        private static HttpClient client = new HttpClient();
        public IEnumerable<Diagnosis> GetAllDiagnoses()
        {
            HttpResponseMessage response = client.GetAsync("https://fysxapi.azurewebsites.net/api/diagnosis").Result;
            IEnumerable<Diagnosis> data = JsonConvert.DeserializeObject<IEnumerable<Diagnosis>>(response.Content.ReadAsStringAsync().Result);
            return data;
        }

        public IEnumerable<string> GetCategories()
        {
            HttpResponseMessage response = client.GetAsync("https://fysxapi.azurewebsites.net/api/diagnosis/categories").Result;
            IEnumerable<string> data = JsonConvert.DeserializeObject<IEnumerable<string>>(response.Content.ReadAsStringAsync().Result);
            return data;
        }

        public IEnumerable<Diagnosis> GetDiagnosesByCategory(string category)
        {
            HttpResponseMessage response = client.GetAsync("https://fysxapi.azurewebsites.net/api/diagnosis/" + category).Result;
            IEnumerable<Diagnosis> data = JsonConvert.DeserializeObject<IEnumerable<Diagnosis>>(response.Content.ReadAsStringAsync().Result);
            return data;
        }

        public Diagnosis GetDiagnosisById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
