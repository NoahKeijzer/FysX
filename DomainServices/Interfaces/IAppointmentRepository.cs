using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DomainServices.Interfaces
{
    public interface IAppointmentRepository
    {
        public List<Appointment> GetAllAppointments();
        public List<Appointment> GetAllAppointmentsForTreator(Treator treator);
        public List<Appointment> GetUpcomingAppointmentsForTreator(Treator treator);
        public List<Appointment> GetAppointmentsTodayForTreator(Treator treator);
        public Appointment GetAppointmentById(int id);
        public List<Appointment> GetUpcomingAppointmentsForPatient(Patient patient);
        public List<Appointment> GetAppointmentsForPatient(Patient patients);
        public List<Appointment> GetAppoinmentsTodayForPatient(Patient patient);
        public void DeleteAppointment(int id);
        public void DeleteAppointment(Appointment appointment);
        public void UpdateAppointment(int id, Appointment updatedAppointment);
        public void AddAppointment(Appointment appointment);

    }
}
