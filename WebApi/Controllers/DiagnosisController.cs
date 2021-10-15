using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Infra;
using WebApi.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosisController : ControllerBase
    {
        private readonly IDiagnosisRepository diagnosisRepository;
        public DiagnosisController(IDiagnosisRepository diagnosisRepository)
        {
            this.diagnosisRepository = diagnosisRepository;
        }
        // GET: api/<DiagnosisController>
        [HttpGet]
        public IEnumerable<Diagnosis> Get()
        {
            //AddAllDiagnosis();
            return diagnosisRepository.GetAllDiagnoses();
        }
        // GET api/<DiagnosisController>/5
        [HttpGet("{id}")]
        public Diagnosis Get(int id)
        {
            return diagnosisRepository.GetDiagnosisById(id);
        }
        [HttpGet("{category}")]
        public IEnumerable<Diagnosis> Get(string category)
        {
            return diagnosisRepository.GetDiagnosesByCategory(category);
        }
        public void AddAllDiagnosis()
        {
            using (var reader = new StreamReader("Vektis lijst diagnoses 3.csv"))
            {
                int i = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (i != 0)
                    {
                        string[] values = line.Split(',');
                        if (values[2].StartsWith("\"") && !values[2].EndsWith("\"") && values.Count() > 3)
                        {
                            for(int j = 3; j < values.Count(); j++)
                            {
                                values[2] = values[2] + "," + values[j];
                            }
                        }
                        
                        values[2] = values[2].Replace("\"", "");
                        Diagnosis d = new Diagnosis(int.Parse(values[0]), values[1], values[2]);
                        diagnosisRepository.AddDiagnosis(d);
                    }
                    i++;
                }
            }
        }

        [HttpGet("categories")]
        public IEnumerable<string> GetCategories()
        {
            return diagnosisRepository.GetCategories();
        }
    }
}