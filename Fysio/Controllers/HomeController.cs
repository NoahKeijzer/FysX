using Fysio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using DomainServices.Interfaces;
using Domain;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Fysio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPatientRepository patientRepository;

        public HomeController(ILogger<HomeController> logger, IPatientRepository patientRepository)
        {
            _logger = logger;
            this.patientRepository = patientRepository;
        }

        [Authorize(Policy = "RequireTreator")]
        public IActionResult Index()
        {
            if (HttpContext.User.HasClaim(c => c.Type == "Claim.Treator"))
            {
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

        [Authorize(Policy = "RequireFysio")]
        [HttpGet]
        public IActionResult PatientForm()
        {
            return View();
        }

        [Authorize(Policy = "RequireFysio")]
        [HttpPost]
        public ViewResult PatientForm(PatientModel patient)
        {
            if (ModelState.IsValid)
            {
                patientRepository.AddPatient(patient.ConvertPatientModelToPatient());
                return View("Index", ConvertPatientToPatientModelList());
            } else
            {
                return View();
            }
        }

        [Authorize(Policy = "RequireTreator")]
        public IActionResult Patient(PatientModel patient, int Id)
        {
            Console.WriteLine(Id);
            if(patient.Email != null)
            {
                return View(patient);
            } else
            {
                if(patientRepository.GetPatientById(Id) != null)
                {
                    return View(ConvertPatientToPatientModel(patientRepository.GetPatientById(Id)));
                }
                return View("Index", ConvertPatientToPatientModelList());
            }
        }

        private PatientModel ConvertPatientToPatientModel(Patient patient)
        {
            bool gender = patient.Gender == Gender.Male ? true : false;
            return new PatientModel() { Birthdate = patient.BirthDate, Email = patient.Email, Name = patient.Name, PhoneNumber = patient.PhoneNumber, RegistrationNumber = patient.RegistrationNumber, Teacher = !patient.Student, Gender = gender, Id = patient.Id};
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
