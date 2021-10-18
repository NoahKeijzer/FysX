using Domain;
using DomainServices.Interfaces;
using DomainServices.Services;
using Fysio.Areas.Treator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Areas.Treator.Controllers
{
    [Area("Treator")]
    [Authorize(Policy = "RequireTreator")]
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
            IdentityUser usr = userManager.GetUserAsync(HttpContext.User).Result;
            string email = usr.Email;
            Domain.Treator t = treatorRepository.GetTreatorByEmail(email);

            IEnumerable<Appointment> appointments = appointmentRepository.GetUpcomingAppointmentsForTreator(t);
            return View(appointments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAppointment(AppointmentModel appointmentModel)
        {
            Domain.Treator t = treatorRepository.GetTreatorByEmail(appointmentModel.TreatorEmail);
            Domain.Patient p = patientRepository.GetPatientById(appointmentModel.PatientId);
            DateTime dateTime = DateTime.Parse(appointmentModel.AppointmentDate + " " + appointmentModel.AppointmentTime);
            PatientFile pf = patientFileRepository.GetCurrentPatientFileForPatient(p);
            int duration = pf.TreatmentPlan.MinutesPerSession;
            Appointment appointment = new Appointment(t, p, dateTime, dateTime.AddMinutes(duration));
            if (appointmentModel.Id != 0)
            {
                if(addAppointmentService.UpdateAppointment(appointment, appointmentModel.Id))
                {
                    return RedirectToAction("Index", "Patient");
                }
                else
                {
                    return View();
                }

            } else
            {
                if (addAppointmentService.AddAppointment(appointment, pf))
                {
                    return RedirectToAction("Index", "Patient");
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpGet]
        public IActionResult AddAppointment(int patientId, int appointmentId)
        {
            List<Domain.Treator> allTreators = treatorRepository.GetAllTreators();
            ViewBag.Treators = from Domain.Treator t in allTreators select new SelectListItem { Value = t.Email, Text = t.Name };

            List<Domain.Patient> allPatients = patientRepository.GetAllPatients();
            ViewBag.Patients = from Domain.Patient p in allPatients select new SelectListItem { Value = p.Id.ToString(), Text = p.Name };

            IdentityUser usr = userManager.GetUserAsync(HttpContext.User).Result;
            string email = usr.Email;
            if(appointmentId != 0)
            {
                Appointment a = appointmentRepository.GetAppointmentById(appointmentId);
                ViewBag.IsNew = false;
                return View(new AppointmentModel() { Id = a.Id, TreatorEmail = email, PatientId = patientId, AppointmentDate = a.AppointmentDateTime.Date.ToString(), AppointmentTime = a.AppointmentDateTime.TimeOfDay.ToString() });
            } else
            {
                ViewBag.IsNew = true;
                return View(new AppointmentModel() { TreatorEmail = email, PatientId = patientId });
            }
            
        }

        public IActionResult DeleteAppointment(int id)
        {
            Appointment a = appointmentRepository.GetAppointmentById(id);
            addAppointmentService.DeleteAppointment(a);
            return RedirectToAction("PatientDetail", "Patient", new { id = a.Patient.Id });
        }

        [HttpPost]
        [Route("/Appointment/GetPossibleAppointmentTimes/{treatorEmail}/{patientId}/{date}")]
        public JsonResult GetPossibleAppointmentTimes(string treatorEmail, int patientId, string date)
        {
            if(!(treatorEmail.Equals("undefined") && patientId.Equals("undefined") && date.Equals("undefined")))
            {
                Domain.Treator treator = treatorRepository.GetTreatorByEmail(treatorEmail);
                Domain.Patient patient = patientRepository.GetPatientById(patientId);
                PatientFile pf = patientFileRepository.GetCurrentPatientFileForPatient(patient);
                DateTime dateObject = DateTime.Parse(date);


                return Json(ConvertTimeToString(addAppointmentService.GetPossibleTimesOnDate(treator, pf, dateObject)));
            }else
            {
                return Json("");
            }
        }

        [Route("/Appointment/ReloadAppointmentDetail/{id}")]
        public IActionResult ReloadAppointmentDetail(int id)
        {
            return ViewComponent("AppointmentDetail", id);
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
