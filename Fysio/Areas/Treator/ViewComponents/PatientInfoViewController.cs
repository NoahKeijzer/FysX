using Fysio.Areas.Treator.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Areas.Treator.ViewComponents
{
    [Area("ViewComponents")]
    public class PatientInfoViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(PatientModel patient)
        {
            return View(patient);
        }
    }
}
