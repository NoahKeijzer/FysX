using Domain;
using DomainServices.Interfaces;
using DomainServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFInfrastructure
{
    public class DBAddTreatmentService : AddTreatmentService
    {
        private readonly ITreatmentTypeRepository treatmentTypeRepository;
        private readonly ITreatmentRepository treatmentRepository;

        public DBAddTreatmentService(ITreatmentTypeRepository treatmentTypeRepository, ITreatmentRepository treatmentRepository)
        {
            this.treatmentTypeRepository = treatmentTypeRepository;
            this.treatmentRepository = treatmentRepository;
        }

        public bool AddTreatment(Treatment t)
        {
            if(t.Patient != null)
            {
                TreatmentType treatmentType = treatmentTypeRepository.GetTreatmentById(t.Type);
                if (treatmentType.RequireExplanation)
                {
                    if(t.Description == null)
                    {
                        return false;
                    } else
                    {
                        treatmentRepository.AddTreatment(t);
                        return true;
                    }
                } else
                {
                    treatmentRepository.AddTreatment(t);
                    return true;
                }
            } else
            {
                return false;
            }
        }
    }
}
