using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Services
{
    public interface AddAppointmentService
    {
        public bool AddAppointment(Appointment appointment, PatientFile patientFile);
        public bool UpdateAppointment(Appointment appointment, int id);
        public bool DeleteAppointment(Appointment appointment);
    }
}
