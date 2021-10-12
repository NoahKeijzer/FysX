using Domain;
using DomainServices.Interfaces;
using DomainServices.Services;
using Fysio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IPatientRepository patientRepository;
        private readonly IPatientFileRepository patientFileRepository;
        private readonly AddAppointmentService addAppointmentService;
        private readonly ITreatorRepository treatorRepository;
        private readonly UserManager<IdentityUser> userManager;

        public AppointmentController(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository, IPatientFileRepository patientFileRepository, AddAppointmentService addAppointmentService, ITreatorRepository treatorRepository, UserManager<IdentityUser> userManager)
        {
            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository;
            this.patientFileRepository = patientFileRepository;
            this.addAppointmentService = addAppointmentService;
            this.treatorRepository = treatorRepository;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAppointment(AppointmentModel appointmentModel)
        {
            Treator t = treatorRepository.GetTreatorByEmail(appointmentModel.TreatorEmail);
            Patient p = patientRepository.GetPatientById(appointmentModel.PatientId);
            DateTime dateTime = DateTime.Parse(appointmentModel.AppointmentDate + " " + appointmentModel.AppointmentTime);
            PatientFile pf = patientFileRepository.GetCurrentPatientFileForPatient(p);
            int duration = pf.TreatmentPlan.MinutesPerSession;
            Appointment appointment = new Appointment(t, p, dateTime, dateTime.AddMinutes(duration));

            if(addAppointmentService.AddAppointment(appointment, pf))
            {
                return RedirectToAction("Index", "Patient");
            } else
            {
                return View();
            }
            
        }

        [HttpGet]
        [Route("Appointment/AddAppointment/{patientId}")]
        [Route("Appointment/AddAppointment")]
        public IActionResult AddAppointment(int patientId)
        {
            List<Treator> allTreators = treatorRepository.GetAllTreators();
            ViewBag.Treators = from Treator t in allTreators select new SelectListItem { Value = t.Email, Text = t.Name};

            List<Patient> allPatients = patientRepository.GetAllPatients();
            ViewBag.Patients = from Patient p in allPatients select new SelectListItem { Value = p.Id.ToString(), Text = p.Name };

            IdentityUser usr = userManager.GetUserAsync(HttpContext.User).Result;
            string email = usr.Email;

            return View(new AppointmentModel() { TreatorEmail = email, PatientId = patientId });
        }

        [HttpPost]
        [Route("/Appointment/GetPossibleAppointmentTimes/{treatorEmail}/{patientId}/{date}")]
        public JsonResult GetPossibleAppointmentTimes(string treatorEmail, int patientId, string date)
        {
            if(!(treatorEmail.Equals("undefined") && patientId.Equals("undefined") && date.Equals("undefined")))
            {
                Treator treator = treatorRepository.GetTreatorByEmail(treatorEmail);
                Patient patient = patientRepository.GetPatientById(patientId);
                PatientFile pf = patientFileRepository.GetCurrentPatientFileForPatient(patient);
                DateTime dateObject = DateTime.Parse(date);


                return Json(ConvertTimeToString(addAppointmentService.GetPossibleTimesOnDate(treator, pf, dateObject)));
            }else
            {
                return Json("");
            }
        }
        
        private IEnumerable<string> ConvertTimeToString(IEnumerable<DateTime> times)
        {
            List<string> strings = new List<string>();
            foreach(DateTime time in times)
            {
                strings.Add(time.ToShortTimeString());
            }
            return strings;
        }
    }
}
