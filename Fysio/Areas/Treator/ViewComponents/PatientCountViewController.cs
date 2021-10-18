using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainServices.Interfaces;
using EFInfrastructure;

namespace Fysio.Areas.Treator.ViewComponents
{
    [Area("ViewComponents")]
    public class PatientCountViewComponent : ViewComponent
    {
        private readonly IPatientRepository PatientRepository;

        public PatientCountViewComponent(IPatientRepository PatientRepository)
        {
            this.PatientRepository = PatientRepository;
        }

        public IViewComponentResult Invoke()
        {
            var result = PatientRepository.AmountOfPatients();
            return View(result);
        }
    }
}
