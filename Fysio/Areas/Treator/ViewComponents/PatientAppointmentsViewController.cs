using Fysio.Areas.Treator.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using DomainServices.Interfaces;
using System.Security.Claims;

namespace Fysio.Areas.Treator.ViewComponents
{
    [Area("Treator")]
    public class PatientAppointmentsViewComponent : ViewComponent
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IPatientRepository patientRepository;

        public PatientAppointmentsViewComponent(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository;
        }

        public IViewComponentResult Invoke(PatientModel patient)
        {
            ViewBag.IsTreator = !HttpContext.User.HasClaim("Claim.Patient", "Patient");
            ViewBag.PatientId = patient.Id;
            List<Appointment> appointments = appointmentRepository.GetUpcomingAppointmentsForPatient(patientRepository.GetPatientById(patient.Id));
            if(appointments.Count > 0)
            {
                ViewBag.HasAppointments = true;
                return View(appointments);

            }else
            {
                ViewBag.HasAppointments = false;
                return View(appointments);
            }
        }
    }
}
