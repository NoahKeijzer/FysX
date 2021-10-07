using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Models
{
    public class CommentModel
    {
        [Required(ErrorMessage = "Zichtbaarheid is verplicht")]
        public bool VisibleForPatient { get; set; }
        [Required(ErrorMessage = "Een opmerking is verplicht")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Er is iets fout gegaan. Probeer het later nog eens.")]
        public int PatientFileId { get; set; }
        public CommentModel()
        {

        }
        public CommentModel(bool visibleForPatient, string description, int patientFileId)
        {
            VisibleForPatient = visibleForPatient;
            Description = description;
            PatientFileId = patientFileId;
        }
    }
}
