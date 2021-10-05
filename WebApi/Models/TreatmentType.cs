using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class TreatmentType
    {
        [Key]
        public int Id { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
        public bool RequireExplanation { get; set; }

        public TreatmentType(int value, string description, bool requireExplanation)
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
