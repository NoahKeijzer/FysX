using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using DomainServices.Interfaces;
using Fysio.Models;

namespace Fysio.Controllers
{
    public class PatientFileController : Controller
    {
        private readonly IPatientFileRepository patientFileRepository;
        private readonly IPatientRepository patientRepository;

        public PatientFileController(IPatientFileRepository repository, IPatientRepository patientRepository)
        {
            patientFileRepository = repository;
            this.patientRepository = patientRepository;
        }

        [Route("PatientFile/{id}")]
        public IActionResult Index(int id, PatientFile patientFile)
        {
            
            if(patientFile.StartDate != DateTime.MinValue)
            {
                return View(patientFile);
            } else
            {
                PatientFile pf = patientFileRepository.GetCurrentPatientFileForPatient(patientRepository.GetPatientById(id));
                return View(pf);
            }
        }
    }
}
