using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Models
{
    public class CommentModel
    {
        public bool VisibleForPatient { get; set; }
        public string Description { get; set; }
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
