using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainServices.Interfaces;
using EFInfrastructure;

namespace Fysio.Areas.Treator.ViewComponents
{
    [Area("Treator")]
    public class PatientCountViewComponent : ViewComponent
    {
        private readonly IPatientRepository PatientRepository;
        private readonly IAppointmentRepository appointmentRepository;

        public PatientCountViewComponent(IPatientRepository PatientRepository, IAppointmentRepository appointmentRepository)
        {
            this.PatientRepository = PatientRepository;
            this.appointmentRepository = appointmentRepository;
        }

        public IViewComponentResult Invoke()
        {
            var result = appointmentRepository.GetAppointmentCountForToday();
            return View(result);
        }
    }
}
