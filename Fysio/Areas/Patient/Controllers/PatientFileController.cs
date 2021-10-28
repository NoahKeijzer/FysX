using Domain;
using DomainServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Authorize(Policy = "RequirePatient")]
    public class PatientFileController : Controller
    {
        private readonly IPatientRepository patientRepository;
        private readonly IPatientFileRepository patientFileRepository;
        private readonly ITreatmentRepository treatmentRepository;
        private readonly UserManager<IdentityUser> userManager;

        public PatientFileController(IPatientRepository patientRepository, IPatientFileRepository patientFileRepository, ITreatmentRepository treatmentRepository, UserManager<IdentityUser> userManager)
        {
            this.patientRepository = patientRepository;
            this.patientFileRepository = patientFileRepository;
            this.userManager = userManager;
            this.treatmentRepository = treatmentRepository;
        }

        public IActionResult Index()
        {
            IdentityUser usr = userManager.GetUserAsync(HttpContext.User).Result;
            string email = usr.Email;
            Domain.Patient p = patientRepository.GetPatientByEmail(email);

            PatientFile pf = patientFileRepository.GetCurrentPatientFileForPatient(p);
            if(pf != null)
            {
                return View(pf);
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult TreatmentDetail(int id)
        {
            Treatment t = treatmentRepository.GetTreatmentById(id);
            return View(t);
        }
    }
}
