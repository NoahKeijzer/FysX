using Domain;
using DomainServices.Interfaces;
using DomainServices.Services;
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

        public AppointmentController(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository, IPatientFileRepository patientFileRepository, AddAppointmentService addAppointmentService)
        {
            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository;
            this.patientFileRepository = patientFileRepository;
            this.addAppointmentService = addAppointmentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAppointment(Appointment appointment)
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddAppointment()
        {
            PatientFile pf = patientFileRepository.GetPatientFileById(1);
            List<DateTime> dates = (List<DateTime>) addAppointmentService.GetPossibleTimesOnDate(pf.MainTreator, pf, DateTime.Now.AddDays(1).AddHours(-10));
            List<SelectListItem> items = new List<SelectListItem>();
            foreach(DateTime dateTime in dates)
            {
                items.Add(new SelectListItem(dateTime.ToShortTimeString(), dateTime.ToString()));
            }

            ViewBag.possibleAppointments = items;
            return View();
        }
    }
}
