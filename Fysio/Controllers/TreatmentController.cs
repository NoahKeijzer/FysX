using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fysio.Models;
using Domain;
using DomainServices.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Fysio.Controllers
{
    public class TreatmentController : Controller
    {
        private readonly ITreatmentRepository treatmentRepository;
        private readonly ITreatorRepository treatorRepository;
        private readonly IPatientRepository patientRepository;
        private readonly IPatientFileRepository patientFileRepository;
        private readonly UserManager<IdentityUser> userManager;

        public TreatmentController(ITreatmentRepository treatmentRepository, ITreatorRepository treatorRepository, IPatientRepository patientRepository, IPatientFileRepository patientFileRepository, UserManager<IdentityUser> userManager)
        {
            this.treatmentRepository = treatmentRepository;
            this.treatorRepository = treatorRepository;
            this.patientRepository = patientRepository;
            this.patientFileRepository = patientFileRepository;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        [Route("/Treatment/AddTreatment/{p}")]
        public IActionResult AddTreatment(string p)
        {
            return View(new TreatmentModel() { PatientEmail = p});
        }

        [HttpPost]
        public ActionResult AddTreatment(TreatmentModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser usr = userManager.GetUserAsync(HttpContext.User).Result;
                string email = usr.Email;
                Treator treator = treatorRepository.GetTreatorByEmail(email);

                Patient p = patientRepository.GetPatientByEmail(model.PatientEmail);

                Treatment t = new Treatment(model.Type, model.Description, model.Location, model.Particularities, treator, p, DateTime.Now);
                treatmentRepository.AddTreatment(t);

                PatientFile pf = patientFileRepository.GetCurrentPatientFileForPatient(p);
                pf.Treatments.Add(t);
                patientFileRepository.UpdatePatientFile(pf.Id, pf);
                return (ActionResult)ToPatientList();
            } else
            {
                return View();
            }
        }

        public IActionResult ToPatientList()
        {
            return RedirectToAction("Index", "Patient");
        }
    }
}
