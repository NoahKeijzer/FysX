using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Diagnosis
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string BodyPart { get; set; }
        public string DiagnosisDescription { get; set; }

        public Diagnosis(int value, string bodyPart, string diagnosisDescription)
        {
            Value = value;
            BodyPart = bodyPart;
            DiagnosisDescription = diagnosisDescription;
        }

        public Diagnosis()
        {

        }
    }
}
