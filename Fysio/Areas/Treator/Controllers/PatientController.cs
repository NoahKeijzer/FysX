using DomainServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fysio.Areas.Treator.Models;
using Domain;
using EFInfrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Fysio.Areas.Treator.Controllers
{
    [Area("Treator")]
    [Authorize (Policy = "RequireTreator")]
    public class PatientController : Controller
    {

        private readonly IPatientRepository patientRepository;
        private readonly ITreatorRepository treatorRepository;
        private readonly IPatientFileRepository patientFileRepository;
        private readonly ITreatmentPlanRepository treatmentPlanRepository;
        private readonly IDiagnosisRepository diagnosisRepository;
        private readonly UserManager<IdentityUser> userManager;

        public PatientController(IPatientRepository patientRepository, ITreatorRepository treatorRepository, IPatientFileRepository patientFileRepository, ITreatmentPlanRepository treatmentPlanRepository, UserManager<IdentityUser> userManager, IDiagnosisRepository diagnosisRepository)
        {
            this.patientRepository = patientRepository;
            this.treatorRepository = treatorRepository;
            this.patientFileRepository = patientFileRepository;
            this.treatmentPlanRepository = treatmentPlanRepository;
            this.diagnosisRepository = diagnosisRepository;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(ConvertPatientToPatientModelList());
        }

        [Authorize(Policy = "RequireFysio")]
        [HttpGet]
        public IActionResult AddPatient(int id)
        {
            ViewBag.IsNew = id != 0 ? false : true;
            if(id != 0)
            {
                Domain.Patient p = patientRepository.GetPatientById(id);
                PatientModel model = new PatientModel() { Id = id, PhoneNumber = p.PhoneNumber, Birthdate = p.BirthDate, Email = p.Email, Name = p.Name, RegistrationNumber = p.RegistrationNumber, Teacher = !p.Student, Gender = p.Gender == Gender.Male ? true : false };
                return View(model);
            } else
            {
                return View();
            }
        }


        [Authorize(Policy = "RequireFysio")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPatient(PatientModel patient)
        {
            if (ModelState.IsValid)
            {
                if(patient.Id != 0)
                {
                    Domain.Patient p = patient.ConvertPatientModelToPatient();
                    patientRepository.UpdatePatient(patient.Id, p);
                    ViewBag.Categories = GetAllDiagnoseCategories();
                    return RedirectToAction("PatientDetail", ConvertPatientToPatientModel(patientRepository.GetPatientById(patient.Id)));

                } else
                {
                    Domain.Patient p = patient.ConvertPatientModelToPatient();
                    patientRepository.AddPatient(p);
                    AddTreatorsToViewBag();
                    ViewBag.Categories = GetAllDiagnoseCategories();
                    return View("AddPatientFile", new PatientFileModel() { PatientEmail = p.Email });
                }
            }
            else
            {
                ViewBag.IsNew = patient.Id != 0 ? false : true;
                return View();
            }
        }

        public IActionResult PatientDetail(int id, PatientModel patient)
        {
            ViewBag.IsTreator = true;
            if (patient.Email != null)
            {
                return View(patient);
            }
            else
            {
                if (patientRepository.GetPatientById(id) != null)
                {
                    return View(ConvertPatientToPatientModel(patientRepository.GetPatientById(id)));
                }
                return Index();
            }
        }

        [HttpGet]
        [Authorize(Policy = "RequireFysio")]
        public IActionResult AddPatientfile()
        {
            ViewBag.Categories = GetAllDiagnoseCategories();
            AddTreatorsToViewBag();
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "RequireFysio")]
        [ValidateAntiForgeryToken]
        public ViewResult AddPatientFile(PatientFileModel patientFileModel)
        {
            if (ModelState.IsValid)
            {
                TreatmentPlan treatmentPlan = new TreatmentPlan(patientFileModel.TreatmentsPerWeek, patientFileModel.MinutesPerSession);
                treatmentPlanRepository.AddTreatmentPlan(treatmentPlan);

                ClaimsPrincipal currentUser = this.User;
                string email = currentUser.FindFirst(ClaimTypes.Email).Value;
                Domain.Treator intaker = treatorRepository.GetTreatorByEmail(email);

                Domain.Patient p = patientRepository.GetPatientByEmail(patientFileModel.PatientEmail);

                Diagnosis d = diagnosisRepository.GetDiagnosisById(int.Parse(patientFileModel.DiagnosisCode), Response.HttpContext.Request.Cookies["apiToken"]);

                PatientFile patientFile = new PatientFile(patientFileModel.Complaints, d.Value.ToString(), d.DiagnosisDescription, intaker, null, treatorRepository.GetTreatorByEmail(patientFileModel.TreatorEmail), DateTime.Now, DateTime.MinValue, treatmentPlan, new List<Treatment>(), new List<Comment>(), CalculateAge(p), p);
                patientFileRepository.AddPatientFile(patientFile);
                return View("Index", ConvertPatientToPatientModelList());
            } else
            {
                return View();
            }
        }


        private PatientModel ConvertPatientToPatientModel(Domain.Patient patient)
        {
            bool gender = patient.Gender == Gender.Male ? true : false;
            PatientModel model = new PatientModel() { Birthdate = patient.BirthDate, Email = patient.Email, Name = patient.Name, PhoneNumber = patient.PhoneNumber, RegistrationNumber = patient.RegistrationNumber, Teacher = !patient.Student, Gender = gender, Id = patient.Id };
            if (patient.Image != null)
            {
                model.ImageSrc = "data:image/gif;base64," + Convert.ToBase64String(patient.Image);
            }
            return model;
        }

        private List<PatientModel> ConvertPatientToPatientModelList()
        {
            List<Domain.Patient> patients = patientRepository.GetAllPatients();
            List<PatientModel> patientModels = new List<PatientModel>();
            foreach (Domain.Patient p in patients)
            {
                patientModels.Add(ConvertPatientToPatientModel(p));
            }
            return patientModels;
        }

        private void AddTreatorsToViewBag()
        {
            List<SelectListItem> treators = new List<SelectListItem>();
            List<FysioTherapist> allTreators = treatorRepository.GetAllFysios();
            foreach (FysioTherapist t in allTreators)
            {
                treators.Add(new SelectListItem() { Value = t.Email, Text = t.Name + " (Fysio)" });
            }

            ViewBag.Treators = treators;
        }

        private IEnumerable<SelectListItem> GetAllDiagnoseCategories()
        {
            return diagnosisRepository.GetCategories(Response.HttpContext.Request.Cookies["apiToken"]).Select(c => new SelectListItem() { Text = c, Value = c });
        }

        public JsonResult GetDiagnosisForCategory(string category)
        {
            return Json(diagnosisRepository.GetDiagnosesByCategory(category, Response.HttpContext.Request.Cookies["apiToken"]));
        }

        private int CalculateAge(Domain.Patient patient)
        {
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int dob = int.Parse(patient.BirthDate.ToString("yyyyMMdd"));
            return (now - dob) / 10000;
        }

    }
}
