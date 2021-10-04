using DomainServices.Interfaces;
using Fysio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Fysio.ViewComponents
{
    public class PatientTreatmentInfoViewComponent : ViewComponent
    {
        private readonly IPatientRepository context;
        private readonly ITreatmentRepository treatmentRepository;
        private readonly IPatientFileRepository patientFileRepository;
        private readonly ITreatorRepository treatorRepository;

        public PatientTreatmentInfoViewComponent(IPatientRepository repository, ITreatmentRepository treatmentRepository, IPatientFileRepository patientFileRepository, ITreatorRepository treatorRepository)
        {
            context = repository;
            this.treatmentRepository = treatmentRepository;
            this.patientFileRepository = patientFileRepository;
            this.treatorRepository = treatorRepository;
        }

        public IViewComponentResult Invoke(PatientModel patient)
        {
            var file = patientFileRepository.GetCurrentPatientFileForPatient(context.GetPatientById(patient.Id));
            if (file != null)
            {
                //Treatment t = new Treatment("blabla", "blabla", "blabla", "blabla", treatorRepository.GetTreatorByEmail("bbuijsen@gmail.com"), context.GetPatientById(patient.Id), DateTime.Now);
                //treatmentRepository.AddTreatment(t);
                //file.Treatments.Add(t);
                //patientFileRepository.UpdatePatientFile(file.Id, file);
                return View(file);
            }
            else
            {
                return View();
            }
        }
    }
}
