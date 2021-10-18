using Domain;
using DomainServices.Interfaces;
using Fysio.Areas.Treator.Models;
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
    public class HomeController : Controller
    {
        private readonly IPatientRepository patientRepository;
        private readonly UserManager<IdentityUser> userManager;

        public HomeController(IPatientRepository patientRepository, UserManager<IdentityUser> userManager)
        {
            this.patientRepository = patientRepository;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            IdentityUser usr = userManager.GetUserAsync(HttpContext.User).Result;
            string email = usr.Email;
            Domain.Patient p = patientRepository.GetPatientByEmail(email);
            return View("Index", ConvertPatientToPatientModel(p));
        }

        private PatientModel ConvertPatientToPatientModel(Domain.Patient patient)
        {
            bool gender = patient.Gender == Gender.Male ? true : false;
            PatientModel model = new PatientModel() { Birthdate = patient.BirthDate, Email = patient.Email, Name = patient.Name, PhoneNumber = patient.PhoneNumber, RegistrationNumber = patient.RegistrationNumber, Teacher = !patient.Student, Gender = gender, Id = patient.Id };
            if (patient.Image != null)
            {
                model.ImageSrc = "data:image/gif;base64," + Convert.ToBase64String(patient.Image);
            }
            return model;
        }
    }
}
