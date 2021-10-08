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
    public class TreatmentController : ControllerBase
    {
        private readonly ITreatmentRepository treatmentRepository;

        public TreatmentController(ITreatmentRepository repository)
        {
            this.treatmentRepository = repository;
        }

        // GET: api/<TreatmentController>
        [HttpGet]
        public IEnumerable<TreatmentType> Get()
        {
            AddAllTreatments();
            return treatmentRepository.GetAllTreatments();
        }

        // GET api/<TreatmentController>/5
        [HttpGet("{id}")]
        public TreatmentType Get(string id)
        {
            return treatmentRepository.GetTreatmentById(id);
        }

        public void AddAllTreatments()
        {
            using (var reader = new StreamReader("Vektis lijst verrichtingen.csv"))
            {
                int i = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (i != 0)
                    {
                        string[] values = line.Split(',');
                        if (values[1].StartsWith("\"") && !values[1].EndsWith("\"") && values.Count() == 4)
                        {
                            values[1] = values[1] + "," + values[2];
                            values[2] = values[3];
                        }
                        TreatmentType t = new TreatmentType(values[0], values[1], values[2].Equals("Ja") ? true : false);
                        treatmentRepository.AddTreatment(t);
                    }
                    i++;
                }
            }
        }

    }
}
