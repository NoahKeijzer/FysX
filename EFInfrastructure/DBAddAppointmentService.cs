using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DomainServices.Services;
using DomainServices.Interfaces;

namespace EFInfrastructure
{
    public class DBAddAppointmentService : AddAppointmentService
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IPatientFileRepository patientFileRepository;
        private readonly IAvailabilityRepository availabilityRepository;

        public DBAddAppointmentService(IAppointmentRepository appointmentRepository, IPatientFileRepository patientFileRepository, IAvailabilityRepository availabilityRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.patientFileRepository = patientFileRepository;
            this.availabilityRepository = availabilityRepository;
        }

        public bool AddAppointment(Appointment appointment, PatientFile patientFile)
        {
            Availability availabilityTreator = availabilityRepository.GetAvailabilityForTreator(appointment.Treator);
            List<Appointment> appointmentsOnDay = appointmentRepository.GetAppointmentsForDateForTreator(appointment.Treator, appointment.AppointmentDateTime);
            List<Appointment> appointmentsForPatient = appointmentRepository.GetAppointmentsForDateForPatient(appointment.Patient, appointment.AppointmentDateTime);
            bool IsAvailable = true;
            foreach(Appointment a in appointmentsOnDay)
            {
                if(!(a.AppointmentDateTime.TimeOfDay > appointment.EndDateTime.TimeOfDay || a.EndDateTime.TimeOfDay < appointment.AppointmentDateTime.TimeOfDay))
                {
                    IsAvailable = false;   
                }
            }
            foreach(Appointment a in appointmentsForPatient)
            {
                if(!(a.AppointmentDateTime.TimeOfDay > appointment.EndDateTime.TimeOfDay || a.EndDateTime.TimeOfDay < appointment.AppointmentDateTime.TimeOfDay))
                {
                    IsAvailable = false;
                }
            }
            if (IsAvailable)
            {
                if (appointment.AppointmentDateTime.DayOfWeek == System.DayOfWeek.Monday)
                {
                    if (appointment.AppointmentDateTime.TimeOfDay > availabilityTreator.MOStartTime.TimeOfDay && appointment.EndDateTime.TimeOfDay < availabilityTreator.MOEndTime.TimeOfDay)
                    {
                        appointmentRepository.AddAppointment(appointment);
                        return true;
                    }
                }
                else if (appointment.AppointmentDateTime.DayOfWeek == System.DayOfWeek.Tuesday)
                {
                    if (appointment.AppointmentDateTime.TimeOfDay > availabilityTreator.TUStartTime.TimeOfDay && appointment.EndDateTime.TimeOfDay < availabilityTreator.TUEndTime.TimeOfDay)
                    {
                        appointmentRepository.AddAppointment(appointment);
                        return true;
                    }
                }
                else if (appointment.AppointmentDateTime.DayOfWeek == System.DayOfWeek.Wednesday)
                {
                    if (appointment.AppointmentDateTime.TimeOfDay > availabilityTreator.WEStartTime.TimeOfDay && appointment.EndDateTime.TimeOfDay < availabilityTreator.WEEndTime.TimeOfDay)
                    {
                        appointmentRepository.AddAppointment(appointment);
                        return true;
                    }
                }
                else if (appointment.AppointmentDateTime.DayOfWeek == System.DayOfWeek.Thursday)
                {
                    if (appointment.AppointmentDateTime.TimeOfDay > availabilityTreator.THStartTime.TimeOfDay && appointment.EndDateTime.TimeOfDay < availabilityTreator.THEndTime.TimeOfDay)
                    {
                        appointmentRepository.AddAppointment(appointment);
                        return true;
                    }
                }
                else if (appointment.AppointmentDateTime.DayOfWeek == System.DayOfWeek.Friday)
                {
                    if (appointment.AppointmentDateTime.TimeOfDay > availabilityTreator.FRStartTime.TimeOfDay && appointment.EndDateTime.TimeOfDay < availabilityTreator.FREndTime.TimeOfDay)
                    {
                        appointmentRepository.AddAppointment(appointment);
                        return true;
                    }
                }
            }
            
            return false;
        }

        public bool DeleteAppointment(Appointment appointment)
        {
            if(appointment.AppointmentDateTime < DateTime.Now.AddDays(-1))
            {
                appointmentRepository.DeleteAppointment(appointment);
                return true;
            }
            return false;
        }

        public bool UpdateAppointment(Appointment appointment, int id)
        {
            if (appointment.AppointmentDateTime < DateTime.Now.AddDays(-1))
            {
                appointmentRepository.UpdateAppointment(id, appointment);
                return true;
            }
            return false;
        }
    }
}
