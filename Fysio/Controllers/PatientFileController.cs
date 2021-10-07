using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using DomainServices.Interfaces;
using Fysio.Models;
using System.Security.Claims;

namespace Fysio.Controllers
{
    public class PatientFileController : Controller
    {
        private readonly IPatientFileRepository patientFileRepository;
        private readonly IPatientRepository patientRepository;
        private readonly ITreatorRepository treatorRepository;
        private readonly ICommentRepository commentRepository;

        public PatientFileController(IPatientFileRepository repository, IPatientRepository patientRepository, ITreatorRepository treatorRepository, ICommentRepository commentRepository)
        {
            patientFileRepository = repository;
            this.patientRepository = patientRepository;
            this.treatorRepository = treatorRepository;
            this.commentRepository = commentRepository;
        }

        [Route("PatientFile/{id}")]
        public IActionResult Index(int id, PatientFile patientFile)
        {
            if(patientFile != null)
            {
                if(patientFile.StartDate != DateTime.MinValue && patientFile.Patient != null)
                {
                    return View(patientFile);
                } else
                {
                    if (patientFile.Age != 0)
                    {
                        PatientFile pf = patientFileRepository.GetPatientFileById(patientFile.Id);
                        return View(pf);
                    } else
                    {
                        PatientFile pf = patientFileRepository.GetCurrentPatientFileForPatient(patientRepository.GetPatientById(id));
                        return View(pf);
                    }
                }
            } else
            {
                return View(patientFile);
            }
        }

        [HttpGet]
        [Route("/PatientFile/AddComment/{id}")]
        public IActionResult AddComment(int id)
        {
            return View(new CommentModel() { PatientFileId = id});
        }

        [HttpPost]
        public ActionResult AddComment(CommentModel model)
        {
            if (ModelState.IsValid)
            {
                ClaimsPrincipal currentUser = this.User;
                string email = currentUser.FindFirst(ClaimTypes.Email).Value;
                Treator creator = treatorRepository.GetTreatorByEmail(email);

                Comment c = new Comment(model.Description, DateTime.Now, creator, model.VisibleForPatient);
                commentRepository.AddComment(c);

                PatientFile pf = patientFileRepository.GetPatientFileById(model.PatientFileId);
                pf.Comments.Add(c);
                patientFileRepository.UpdatePatientFile(pf);

                return RedirectToAction("Index", "PatientFile", pf);
            } else
            {
                return View(new CommentModel() { PatientFileId = model.PatientFileId });
            }
        }
    }
}
