using Fysio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.ViewComponents
{
    public class PatientInfoViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(PatientModel patient)
        {
            return View(patient);
        }
    }
}
