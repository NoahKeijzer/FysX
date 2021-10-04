using DomainServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fysio.Models;
using Domain;
using EFInfrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fysio.Controllers
{
    [Authorize (Policy = "RequireTreator")]
    public class PatientController : Controller
    {

        private readonly IPatientRepository patientRepository;
        private readonly ITreatorRepository treatorRepository;

        public PatientController(IPatientRepository patientRepository, ITreatorRepository treatorRepository)
        {
            this.patientRepository = patientRepository;
            this.treatorRepository = treatorRepository;
        }

        public IActionResult Index()
        {
            return View(ConvertPatientToPatientModelList());
        }

        [Authorize(Policy = "RequireFysio")]
        [HttpGet]
        public IActionResult AddPatient()
        {
            return View();
        }


        [Authorize(Policy = "RequireFysio")]
        [HttpPost]
        public ViewResult AddPatient(PatientModel patient)
        {
            if (ModelState.IsValid)
            {
                patientRepository.AddPatient(patient.ConvertPatientModelToPatient());
                return View("Index", ConvertPatientToPatientModelList());
            }
            else
            {
                return View();
            }
        }

        [Route("/Patient/PatientDetail/{id}")]
        public IActionResult PatientDetail(PatientModel patient, string id)
        {
            int Id = int.Parse(id);
            if (patient.Email != null)
            {
                return View(patient);
            }
            else
            {
                if (patientRepository.GetPatientById(Id) != null)
                {
                    return View(ConvertPatientToPatientModel(patientRepository.GetPatientById(Id)));
                }
                return Index();
            }
        }

        public IActionResult AddPatientfile()
        {
            List<SelectListItem> treators = new List<SelectListItem>();
            List<FysioTherapist> allTreators = treatorRepository.GetAllFysios();
            foreach(FysioTherapist t in allTreators)
            {
                treators.Add(new SelectListItem() { Value = t.Email, Text = t.Name + " (Fysio)" });
            }

            ViewBag.Treators = treators;
            return View();
        }

        [HttpPost]
        public ViewResult AddPatientFile(PatientFileModel patientFileModel)
        {
            if (ModelState.IsValid)
            {
                //TODO: Patientfile aanmaken in de db en koppelen aan patient etc
                return View("Index", ConvertPatientToPatientModelList());
            } else
            {
                return View();
            }
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
