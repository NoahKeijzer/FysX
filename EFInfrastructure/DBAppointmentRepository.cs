using DomainServices.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFInfrastructure;
using Microsoft.EntityFrameworkCore;

namespace EFInfrastructure
{
    public class DBAppointmentRepository : IAppointmentRepository
    {
        private readonly FysioDbContext _context;
        public DBAppointmentRepository(FysioDbContext context)
        {
            _context = context;
        }
        public void AddAppointment(Appointment appointment)
        {
            // TODO: implementeer controle op beschikbaarheid
            _context.Add(appointment);
            _context.SaveChanges();
        }

        public void DeleteAppointment(int id)
        {
            _context.Remove(id);
            _context.SaveChanges();
        }

        public void DeleteAppointment(Appointment appointment)
        {
            _context.Remove(appointment);
            _context.SaveChanges();
        }

        public List<Appointment> GetAllAppointments()
        {
            return _context.Appointments.ToList();
        }

        public List<Appointment> GetAllAppointmentsForTreator(Treator treator)
        {
            return _context.Appointments.Where(p => p.Treator == treator).ToList();
        }

        public List<Appointment> GetAppoinmentsTodayForPatient(Patient patient)
        {
            return _context.Appointments.Where(p => p.Patient == patient && p.AppointmentDateTime.Date == DateTime.Now.Date).ToList();
        }

        public Appointment GetAppointmentById(int id)
        {
            return _context.Appointments.Where(p => p.Id == id).FirstOrDefault();
        }

        public List<Appointment> GetAppointmentsForDateForPatient(Patient patient, DateTime date)
        {
            return _context.Appointments.Where(p => p.Patient == patient && p.AppointmentDateTime.Date == date.Date).ToList();
        }

        public List<Appointment> GetAppointmentsForDateForTreator(Treator treator, DateTime date)
        {
            return _context.Appointments.Where(p => p.Treator == treator && p.AppointmentDateTime.Date == date.Date).ToList();
        }

        public List<Appointment> GetAppointmentsForPatient(Patient patient)
        {
            return _context.Appointments.Where(p => p.Patient == patient).ToList();
        }

        public List<Appointment> GetAppointmentsTodayForTreator(Treator treator)
        {
            return _context.Appointments.Where(p => p.Treator == treator && p.AppointmentDateTime.Date == DateTime.Now.Date).ToList();
        }

        public List<Appointment> GetUpcomingAppointmentsForPatient(Patient patient)
        {
            return _context.Appointments.Where(p => p.Patient == patient && p.AppointmentDateTime.Date > DateTime.Now).Include(p => p.Treator).OrderBy(p => p.AppointmentDateTime).ToList();
        }

        public List<Appointment> GetUpcomingAppointmentsForTreator(Treator treator)
        {
            return _context.Appointments.OrderBy(p => p.AppointmentDateTime).Where(p => p.Treator == treator && p.AppointmentDateTime > DateTime.Now).Include(p => p.Patient).Include(p => p.Treator).ToList();
        }

        public void UpdateAppointment(int id, Appointment updatedAppointment)
        {
            Appointment old = _context.Appointments.Where(p => p.Id == id).FirstOrDefault();
            old = updatedAppointment;
            _context.SaveChanges();
        }
    }
}
