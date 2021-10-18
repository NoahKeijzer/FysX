using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fysio.Areas.Treator.Models;
using Domain;
using DomainServices.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using DomainServices.Services;

namespace Fysio.Areas.Treator.Controllers
{
    [Area("Treator")]
    [Authorize(Policy = "RequireTreator")]
    public class TreatmentController : Controller
    {
        private readonly ITreatmentRepository treatmentRepository;
        private readonly ITreatorRepository treatorRepository;
        private readonly IPatientRepository patientRepository;
        private readonly IPatientFileRepository patientFileRepository;
        private readonly ITreatmentTypeRepository treatmentTypeRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly AddTreatmentService addTreatmentService;

        public TreatmentController(ITreatmentRepository treatmentRepository, ITreatorRepository treatorRepository, IPatientRepository patientRepository, IPatientFileRepository patientFileRepository, UserManager<IdentityUser> userManager, ITreatmentTypeRepository treatmentTypeRepository, AddTreatmentService addTreatmentService)
        {
            this.treatmentRepository = treatmentRepository;
            this.treatorRepository = treatorRepository;
            this.patientRepository = patientRepository;
            this.patientFileRepository = patientFileRepository;
            this.treatmentTypeRepository = treatmentTypeRepository;
            this.userManager = userManager;
            this.addTreatmentService = addTreatmentService;
        }

        public IActionResult Index(int id)
        {
            Treatment t = treatmentRepository.GetTreatmentById(id);
            return View(t);
        }
        
        [HttpGet]
        public IActionResult AddTreatment(string p, int id)
        {
            Treatment t = treatmentRepository.GetTreatmentById(id);
            ViewBag.TreatmentTypes = GetTreatmentTypes();
            if(t != null)
            {
                ViewBag.IsNew = false;
                TreatmentModel treatmentModel = new TreatmentModel(t.Type, t.Description, t.Location, t.Particularities) { PatientEmail = t.Patient.Email, TreatmentId = t.Id };
                return View(treatmentModel);
            } else
            {
                ViewBag.IsNew = true;
                return View(new TreatmentModel() { PatientEmail = p});
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTreatment(TreatmentModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.TreatmentId != 0)
                {
                    IdentityUser usr = userManager.GetUserAsync(HttpContext.User).Result;
                    string email = usr.Email;
                    Domain.Treator treator = treatorRepository.GetTreatorByEmail(email);

                    Domain.Patient p = patientRepository.GetPatientByEmail(model.PatientEmail);

                    Treatment t = new Treatment(model.Type, model.Description, model.Location, model.Particularities, treator, p, DateTime.Now);
                    treatmentRepository.UpdateTreatment(model.TreatmentId, t);
                    return (ActionResult)RedirectToAction("Index", "PatientFile", p.Id);
                } else
                {
                    IdentityUser usr = userManager.GetUserAsync(HttpContext.User).Result;
                    string email = usr.Email;
                    Domain.Treator treator = treatorRepository.GetTreatorByEmail(email);

                    Domain.Patient p = patientRepository.GetPatientByEmail(model.PatientEmail);

                    Treatment t = new Treatment(model.Type, model.Description, model.Location, model.Particularities, treator, p, DateTime.Now);

                    if (addTreatmentService.AddTreatment(t))
                    {
                        PatientFile pf = patientFileRepository.GetCurrentPatientFileForPatient(p);
                        pf.Treatments.Add(t);
                        patientFileRepository.UpdatePatientFile(pf);
                        return RedirectToAction("Index", "PatientFile", pf);
                    } else
                    {
                        return View("Error");
                    }

                }
            } else
            {
                ViewBag.IsNew = true;
                ViewBag.TreatmentTypes = GetTreatmentTypes();
                return View(model);
            }
        }

        public ActionResult DeleteTreatment(int treatmentId)
        {
            if(treatmentId != 0)
            {
                Treatment t = treatmentRepository.GetTreatmentById(treatmentId);

                Domain.Patient p = patientRepository.GetPatientByEmail(t.Patient.Email);
                PatientFile pf = patientFileRepository.GetCurrentPatientFileForPatient(p);
                
                treatmentRepository.DeleteTreatment(t);

                return RedirectToAction("Index", "PatientFile", pf);
            } else
            {
                ViewBag.IsNew = false;
                return View("Error");
            }
        }

        public IEnumerable<SelectListItem> GetTreatmentTypes()
        {
            
            return treatmentTypeRepository.GetAllTreatments().Select(t => new SelectListItem() { Text = t.Description, Value = t.Value});
        }

        public JsonResult IsDescriptionNecessaryForTreatment(string id)
        {
            return Json(treatmentTypeRepository.GetTreatmentById(id).RequireExplanation);
        }

        public IActionResult ToPatientList()
        {
            return RedirectToAction("Index", "Patient");
        }
    }
}
