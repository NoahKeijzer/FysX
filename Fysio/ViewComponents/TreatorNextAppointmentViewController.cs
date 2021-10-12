using Domain;
using DomainServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.ViewComponents
{
    public class TreatorNextAppointmentViewComponent : ViewComponent
    {
        private readonly IPatientFileRepository patientFileRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly ITreatmentRepository treatmentRepository;

        public TreatorNextAppointmentViewComponent(IPatientFileRepository patientFileRepository, IAppointmentRepository appointmentRepository, ITreatmentRepository treatmentRepository)
        {
            this.patientFileRepository = patientFileRepository;
            this.appointmentRepository = appointmentRepository;
            this.treatmentRepository = treatmentRepository;
        }

        public IViewComponentResult Invoke(Treator treator)
        {
            IEnumerable<Appointment> appointments = appointmentRepository.GetUpcomingAppointmentsForTreator(treator);
            Appointment a = appointments.FirstOrDefault();
            Patient p = a.Patient;
            PatientFile pf = patientFileRepository.GetCurrentPatientFileForPatient(p);
            Treatment lastTreatment = treatmentRepository.GetTreatmentsForPatient(p).OrderByDescending(p => p.TreatmentDateTime).FirstOrDefault();

            ViewBag.LastTreatment = lastTreatment;
            ViewBag.Appointment = a;
            return View(pf);
        }
    }
}
