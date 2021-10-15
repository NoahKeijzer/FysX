using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class TreatmentType
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool RequireExplanation { get; set; }

        public TreatmentType(string value, string description, bool requireExplanation)
        {
            Value = value;
            Description = description;
            RequireExplanation = requireExplanation;
        }

        public TreatmentType()
        {

        }
    }
}
