using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PatientFile
    {
        [Key]
        public int Id { get; set; }
        public string Complaints { get; set; }
        public string DiagnosisCode { get; set; }
        public string DiagnosisDescription { get; set; }
        public Treator Intaker { get; set; }
        public Treator SupervisingTreator { get; set; }
        public Treator MainTreator { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TreatmentPlan TreatmentPlan { get; set; }
        public ICollection<Treatment> Treatments { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public int Age { get; set; }
        public Patient Patient { get; set; }

        public PatientFile(string complaints, string diagnosisCode, string diagnosisDescription, Treator intaker, Treator supervisingTreator, Treator mainTreator, DateTime startDate, DateTime endDate, TreatmentPlan treatmentPlan, ICollection<Treatment> treatments, ICollection<Comment> comments, int age, Patient patient)
        {
            Complaints = complaints;
            DiagnosisCode = diagnosisCode;
            DiagnosisDescription = diagnosisDescription;
            Intaker = intaker;
            SupervisingTreator = supervisingTreator;
            MainTreator = mainTreator;
            StartDate = startDate;
            EndDate = endDate;
            TreatmentPlan = treatmentPlan;
            Treatments = treatments;
            Comments = comments;
            Age = age;
            Patient = patient;
        }

        public PatientFile(TreatmentPlan tp)
        {
            this.TreatmentPlan = tp;
        }

        public PatientFile()
        {

        }
    }
}
