using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using DomainServices.Interfaces;
using Fysio.Models;

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
            
            if(patientFile.StartDate != DateTime.MinValue)
            {
                return View(patientFile);
            } else
            {
                PatientFile pf = patientFileRepository.GetCurrentPatientFileForPatient(patientRepository.GetPatientById(id));
                Treator t = treatorRepository.GetTreatorByEmail("bbuijsen@gmail.com");
                Comment c = new Comment("blablbalbjalba", DateTime.Now, t, false);
                commentRepository.AddComment(c);

                pf.Comments.Add(c);
                patientFileRepository.UpdatePatientFile(pf);

                return View(pf);
            }
        }
    }
}
