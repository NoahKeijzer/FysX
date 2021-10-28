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

        public bool AddTreatment(Treatment t, string token)
        {
            if(t.Patient != null)
            {
                TreatmentType treatmentType = treatmentTypeRepository.GetTreatmentById(t.Type, token);
                t.TypeDescription = treatmentType.Description;
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

        public bool UpdateTreatment(Treatment t, int id, string token)
        {
            if (t.Patient != null)
            {
                TreatmentType treatmentType = treatmentTypeRepository.GetTreatmentById(t.Type, token);
                t.TypeDescription = treatmentType.Description;
                if (treatmentType.RequireExplanation)
                {
                    if (t.Description == null)
                    {
                        return false;
                    }
                    else
                    {
                        treatmentRepository.UpdateTreatment(id, t);
                        return true;
                    }
                }
                else
                {
                    treatmentRepository.UpdateTreatment(id, t);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
