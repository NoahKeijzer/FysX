using Domain;
using DomainServices.Interfaces;
using Fysio.Areas.Treator.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Areas.Treator.ViewComponents
{
    [Area("Treator")]
    public class AppointmentDetailViewComponent : ViewComponent
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IPatientRepository patientRepository;
        private readonly IPatientFileRepository patientFileRepository;

        public AppointmentDetailViewComponent(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository, IPatientFileRepository patientFileRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository;
            this.patientFileRepository = patientFileRepository;
        }

        public IViewComponentResult Invoke(int id)
        {
            Appointment a = appointmentRepository.GetAppointmentById(id);
            PatientFile pf = patientFileRepository.GetCurrentPatientFileForPatient(a.Patient);

            ViewBag.Appointment = a;
            return View(pf);
        }
    }
}
