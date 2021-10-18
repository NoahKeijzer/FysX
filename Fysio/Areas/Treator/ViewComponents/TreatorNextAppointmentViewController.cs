using Domain;
using DomainServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Areas.Treator.ViewComponents
{
    [Area("ViewComponents")]
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

        public IViewComponentResult Invoke(Domain.Treator treator)
        {
            IEnumerable<Appointment> appointments = appointmentRepository.GetUpcomingAppointmentsForTreator(treator);
            Appointment a = appointments.FirstOrDefault();
            Domain.Patient p = null;
            PatientFile pf = null;
            Treatment lastTreatment = null;
            if (a != null)
            {
                p = a.Patient;
                pf = patientFileRepository.GetCurrentPatientFileForPatient(p);
                lastTreatment = treatmentRepository.GetTreatmentsForPatient(p).OrderByDescending(p => p.TreatmentDateTime).FirstOrDefault();
            }

            ViewBag.LastTreatment = lastTreatment;
            ViewBag.Appointment = a;
            return View(pf);
        }
    }
}
