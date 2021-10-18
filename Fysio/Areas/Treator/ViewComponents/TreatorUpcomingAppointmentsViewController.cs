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
    public class TreatorUpcomingAppointmentsViewComponent : ViewComponent
    {
        private readonly ITreatorRepository treatorRepository;
        private readonly IAppointmentRepository appointmentRepository;

        public TreatorUpcomingAppointmentsViewComponent(ITreatorRepository treatorRepository, IAppointmentRepository appointmentRepository)
        {
            this.treatorRepository = treatorRepository;
            this.appointmentRepository = appointmentRepository;
        }

        public IViewComponentResult Invoke(Domain.Treator t)
        { 
            IEnumerable<Appointment> upcomingAppointments = appointmentRepository.GetUpcomingAppointmentsForTreator(t).Take(5);
            return View(upcomingAppointments);
        }
    }
}
