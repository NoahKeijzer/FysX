using Fysio.Areas.Treator.Models;
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

namespace Fysio.Areas.Treator.Controllers
{
    [Area("Treator")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPatientRepository patientRepository;
        private readonly ITreatorRepository treatorRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly AddAppointmentService addAppointmentService;
        private readonly IAvailabilityRepository availabilityRepository;


        public HomeController(ILogger<HomeController> logger, IPatientRepository patientRepository, ITreatorRepository treatorRepository, UserManager<IdentityUser> userManager, AddAppointmentService addAppointmentService, IAvailabilityRepository availabilityRepository)
        {
            _logger = logger;
            this.patientRepository = patientRepository;
            this.treatorRepository = treatorRepository;
            this.userManager = userManager;
            this.addAppointmentService = addAppointmentService;
            this.availabilityRepository = availabilityRepository;
        }

        [Authorize(Policy = "RequireTreator")]
        public IActionResult Index()
        {
            IdentityUser usr = userManager.GetUserAsync(HttpContext.User).Result;
            string email = usr.Email;
            Domain.Treator t = treatorRepository.GetTreatorByEmail(email);

            ViewBag.Name = t.Name;
            return View(t);
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

        [HttpGet]
        public IActionResult AddAvailability()
        {
            IdentityUser usr = userManager.GetUserAsync(HttpContext.User).Result;
            string email = usr.Email;
            Domain.Treator t = treatorRepository.GetTreatorByEmail(email);
            
            Availability a = availabilityRepository.GetAvailabilityForTreator(t);
            if(a != null)
            {
                AvailabilityModel model = new AvailabilityModel(a.MOStartTime, a.MOEndTime, a.TUStartTime, a.TUEndTime, a.WEStartTime, a.WEEndTime, a.THStartTime, a.THEndTime, a.FRStartTime, a.FREndTime);
                return View(model);
            } else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAvailability(AvailabilityModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser usr = userManager.GetUserAsync(HttpContext.User).Result;
                string email = usr.Email;
                Domain.Treator t = treatorRepository.GetTreatorByEmail(email);

                Availability availability = new Availability(t, model.MOStartTime, model.MOEndTime, model.TUStartTime, model.TUEndTime, model.WEStartTime, model.WEEndTime, model.THStartTime, model.THEndTime, model.FRStartTime, model.FREndTime);
                
                availabilityRepository.AddAvailability(availability);

                return RedirectToAction("Index", "Home");
            } else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult UpdateAccount()
        {
            IdentityUser usr = userManager.GetUserAsync(HttpContext.User).Result;
            string email = usr.Email;
            Domain.Treator t = treatorRepository.GetTreatorByEmail(email);

            if(t is FysioTherapist)
            {
                FysioTherapist f = (FysioTherapist)t;
                AccountModel model = new AccountModel(f.Name, f.PhoneNumber, f.TeacherNumber, f.BIGNumber);
                ViewBag.IsTreator = true;
                return View(model);
            } else
            {
                Student s = (Student)t;
                AccountModel model = new AccountModel(s.Name, "", s.StudentNumber, -1);
                ViewBag.IsTreator = false;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult UpdateAccount(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser usr = userManager.GetUserAsync(HttpContext.User).Result;
                string email = usr.Email;
                Domain.Treator t = treatorRepository.GetTreatorByEmail(email);

                if (t is FysioTherapist)
                {
                    FysioTherapist f = new FysioTherapist(model.Name, t.Email, model.PhoneNumber, model.SchoolNumber, model.BigNumber);
                    treatorRepository.UpdateTreator(f);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Student s = new Student(model.Name, t.Email, model.SchoolNumber);
                    treatorRepository.UpdateTreator(s);
                    return RedirectToAction("Index", "Home");
                }
            } else
            {
                return View();
            }
        }





        private PatientModel ConvertPatientToPatientModel(Domain.Patient patient)
        {
            bool gender = patient.Gender == Gender.Male ? true : false;
            return new PatientModel() { Birthdate = patient.BirthDate, Email = patient.Email, Name = patient.Name, PhoneNumber = patient.PhoneNumber, RegistrationNumber = patient.RegistrationNumber, Teacher = !patient.Student, Gender = gender, Id = patient.Id };
        }

        private List<PatientModel> ConvertPatientToPatientModelList()
        {
            List<Domain.Patient> patients = patientRepository.GetAllPatients();
            List<PatientModel> patientModels = new List<PatientModel>();
            foreach (Domain.Patient p in patients)
            {
                patientModels.Add(ConvertPatientToPatientModel(p));
            }
            return patientModels;
        }



    }
}
