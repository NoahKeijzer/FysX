using Fysio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using DomainServices.Interfaces;
using DomainServices.Services;
using Domain;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Fysio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPatientRepository patientRepository;
        private readonly ITreatorRepository treatorRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly AddAppointmentService addAppointmentService;


        public HomeController(ILogger<HomeController> logger, IPatientRepository patientRepository, ITreatorRepository treatorRepository, UserManager<IdentityUser> userManager, AddAppointmentService addAppointmentService)
        {
            _logger = logger;
            this.patientRepository = patientRepository;
            this.treatorRepository = treatorRepository;
            this.userManager = userManager;
            this.addAppointmentService = addAppointmentService;
        }

        [Authorize(Policy = "RequireTreator")]
        public IActionResult Index()
        {
            if (HttpContext.User.HasClaim(c => c.Type == "Claim.Treator"))
            {
                //ViewBag.Name = treatorRepository.GetTreatorByEmail(User.FindFirstValue(ClaimTypes.Email)).Name;
                return View(ConvertPatientToPatientModelList());
            }
            else
            {
                return View(new List<PatientModel>());
            }
            //return RedirectToAction("Register", "Account");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }






        private PatientModel ConvertPatientToPatientModel(Patient patient)
        {
            bool gender = patient.Gender == Gender.Male ? true : false;
            return new PatientModel() { Birthdate = patient.BirthDate, Email = patient.Email, Name = patient.Name, PhoneNumber = patient.PhoneNumber, RegistrationNumber = patient.RegistrationNumber, Teacher = !patient.Student, Gender = gender, Id = patient.Id };
        }

        private List<PatientModel> ConvertPatientToPatientModelList()
        {
            List<Patient> patients = patientRepository.GetAllPatients();
            List<PatientModel> patientModels = new List<PatientModel>();
            foreach (Patient p in patients)
            {
                patientModels.Add(ConvertPatientToPatientModel(p));
            }
            return patientModels;
        }

    }
}
