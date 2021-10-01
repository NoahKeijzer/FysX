using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public Treator Treator { get; set; }
        public Patient Patient { get; set; }
        public DateTime AppointmentDateTime { get; set; }

        public Appointment(Treator treator, Patient patient, DateTime appointmentDateTime)
        {
            Treator = treator;
            Patient = patient;
            AppointmentDateTime = appointmentDateTime;
        }

        public Appointment()
        {

        }
    }
}
