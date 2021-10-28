using Domain;
using DomainServices.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EFInfrastructure
{
    public class ApiDiagnosisRepository : IDiagnosisRepository
    {
        private static HttpClient client = new HttpClient();

        public ApiDiagnosisRepository()
        {
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public IEnumerable<Diagnosis> GetAllDiagnoses(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = client.GetAsync("https://fysxapi.azurewebsites.net/api/diagnosis").Result;
            IEnumerable<Diagnosis> data = JsonConvert.DeserializeObject<IEnumerable<Diagnosis>>(response.Content.ReadAsStringAsync().Result);
            return data;
        }

        public IEnumerable<string> GetCategories(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = client.GetAsync("https://fysxapi.azurewebsites.net/api/categories").Result;
            IEnumerable<string> data = JsonConvert.DeserializeObject<IEnumerable<string>>(response.Content.ReadAsStringAsync().Result);
            return data;
        }

        public IEnumerable<Diagnosis> GetDiagnosesByCategory(string category, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = client.GetAsync("https://fysxapi.azurewebsites.net/api/diagnosis/?category=" + category).Result;
            IEnumerable<Diagnosis> data = JsonConvert.DeserializeObject<IEnumerable<Diagnosis>>(response.Content.ReadAsStringAsync().Result);
            return data;
        }

        public Diagnosis GetDiagnosisById(int id, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = client.GetAsync("https://fysxapi.azurewebsites.net/api/diagnosis/" + id).Result;
            Diagnosis data = JsonConvert.DeserializeObject<Diagnosis>(response.Content.ReadAsStringAsync().Result);
            return data;
        }
    }
}
