using Fysio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using DomainServices.Interfaces;

namespace Fysio.ViewComponents
{
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
            return View(appointmentRepository.GetUpcomingAppointmentsForPatient(patientRepository.GetPatientById(patient.Id)));
        }
    }
}
