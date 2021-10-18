using DomainServices.Interfaces;
using Fysio.Areas.Treator.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Fysio.Areas.Treator.ViewComponents
{
    [Area("Treator")]
    public class PatientTreatmentInfoViewComponent : ViewComponent
    {
        private readonly IPatientRepository context;
        private readonly ITreatmentRepository treatmentRepository;
        private readonly IPatientFileRepository patientFileRepository;
        private readonly ITreatorRepository treatorRepository;
        private readonly UserManager<IdentityUser> userManager;

        public PatientTreatmentInfoViewComponent(IPatientRepository repository, ITreatmentRepository treatmentRepository, IPatientFileRepository patientFileRepository, ITreatorRepository treatorRepository, UserManager<IdentityUser> user)
        {
            context = repository;
            this.treatmentRepository = treatmentRepository;
            this.patientFileRepository = patientFileRepository;
            this.treatorRepository = treatorRepository;
            this.userManager = user;
        }

        public IViewComponentResult Invoke(PatientModel patient)
        {
            var file = patientFileRepository.GetCurrentPatientFileForPatient(context.GetPatientById(patient.Id));
            IdentityUser usr = userManager.GetUserAsync(HttpContext.User).Result;
            string email = usr.Email;
            Domain.Treator p = treatorRepository.GetTreatorByEmail(email);
            ViewBag.IsTreator = p != null ? true : false;
            if (file != null)
            {
                if(file.Treatments.Count > 0)
                {
                    ViewBag.HasTreatments = true;

                } else
                {
                    ViewBag.HasTreatments = false;
                }
                return View(file);
            }
            else
            {
                ViewBag.HasTreatments = false;
                return View(file);
            }
        }
    }
}
