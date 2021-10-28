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
    public class ApiTreatmentTypeRepository : ITreatmentTypeRepository
    {
        private static HttpClient client = new HttpClient();
        public IEnumerable<TreatmentType> GetAllTreatments(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = client.GetAsync("https://fysxapi.azurewebsites.net/api/treatment").Result;
            IEnumerable<TreatmentType> data = JsonConvert.DeserializeObject<IEnumerable<TreatmentType>>(response.Content.ReadAsStringAsync().Result);
            return data;
        }

        public TreatmentType GetTreatmentById(string id, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = client.GetAsync("https://fysxapi.azurewebsites.net/api/treatment/" + id).Result;
            TreatmentType data = JsonConvert.DeserializeObject<TreatmentType>(response.Content.ReadAsStringAsync().Result);
            return data;
        }
    }
}
