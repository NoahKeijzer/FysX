using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Treatment
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string TypeDescription { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Particularities { get; set; }
        public Treator Treator { get; set; }
        public Patient Patient { get; set; }
        public DateTime TreatmentDateTime { get; set; }

        public Treatment(string type, string description, string location, string particularities, Treator treator, Patient patient, DateTime treatmentDateTime)
        {
            Type = type;
            Description = description;
            Location = location;
            Particularities = particularities;
            Treator = treator;
            Patient = patient;
            TreatmentDateTime = treatmentDateTime;
        }

        public Treatment()
        {

        }
    }
}
