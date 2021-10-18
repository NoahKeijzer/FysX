using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Areas.Treator.Models
{
    public class AppointmentModel
    {
        public int Id { get; set; }

        [Required]
        public string TreatorEmail { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public string AppointmentDate { get; set; }

        [Required]
        public string AppointmentTime { get; set; }
    }
}
