using Domain;
using DomainServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Areas.Treator.ViewComponents
{
    [Area("Treator")]
    public class TreatorAccountInfoViewComponent : ViewComponent
    {
        private readonly ITreatorRepository treatorRepository;
        private readonly IAvailabilityRepository availabilityRepository;

        public TreatorAccountInfoViewComponent(ITreatorRepository treatorRepository, IAvailabilityRepository availabilityRepository)
        {
            this.treatorRepository = treatorRepository;
            this.availabilityRepository = availabilityRepository;
        }

        public IViewComponentResult Invoke(Domain.Treator t)
        {
            Availability availability = availabilityRepository.GetAvailabilityForTreator(t);
            ViewBag.IsFysio = t is FysioTherapist;
            if (t is FysioTherapist therapist) ViewBag.Treator = therapist;
            else ViewBag.Treator = (Student)t;

            return View(availability);
        }
    }
}
