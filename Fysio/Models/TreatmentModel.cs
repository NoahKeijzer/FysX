using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Models
{
    public class TreatmentModel : IValidatableObject
    {
        [Required]
        public string Type { get; set; }
        public string Description { get; set; }
        [Required]
        public bool DescriptionNecessary { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Particularities { get; set; }

        public string PatientEmail { get; set; }

        public int TreatmentId { get; set; }

        public TreatmentModel(string type, string description, string location, string particularities)
        {
            Type = type;
            Description = description;
            Location = location;
            Particularities = particularities;
        }

        public TreatmentModel()
        {

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(DescriptionNecessary && Description == null)
            {
                yield return new ValidationResult("Beschrijving is verplicht bij deze behandeling");
            }
        }
    }
}
