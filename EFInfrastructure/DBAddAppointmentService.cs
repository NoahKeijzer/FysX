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
            IsPossibleTime(appointment.Treator, appointment.AppointmentDateTime, patientFile.TreatmentPlan.MinutesPerSession);
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
                    if (appointment.AppointmentDateTime.TimeOfDay >= availabilityTreator.MOStartTime.TimeOfDay && appointment.EndDateTime.TimeOfDay <= availabilityTreator.MOEndTime.TimeOfDay)
                    {
                        appointmentRepository.AddAppointment(appointment);
                        return true;
                    }
                }
                else if (appointment.AppointmentDateTime.DayOfWeek == System.DayOfWeek.Tuesday)
                {
                    if (appointment.AppointmentDateTime.TimeOfDay >= availabilityTreator.TUStartTime.TimeOfDay && appointment.EndDateTime.TimeOfDay <= availabilityTreator.TUEndTime.TimeOfDay)
                    {
                        appointmentRepository.AddAppointment(appointment);
                        return true;
                    }
                }
                else if (appointment.AppointmentDateTime.DayOfWeek == System.DayOfWeek.Wednesday)
                {
                    if (appointment.AppointmentDateTime.TimeOfDay >= availabilityTreator.WEStartTime.TimeOfDay && appointment.EndDateTime.TimeOfDay <= availabilityTreator.WEEndTime.TimeOfDay)
                    {
                        appointmentRepository.AddAppointment(appointment);
                        return true;
                    }
                }
                else if (appointment.AppointmentDateTime.DayOfWeek == System.DayOfWeek.Thursday)
                {
                    if (appointment.AppointmentDateTime.TimeOfDay >= availabilityTreator.THStartTime.TimeOfDay && appointment.EndDateTime.TimeOfDay <= availabilityTreator.THEndTime.TimeOfDay)
                    {
                        appointmentRepository.AddAppointment(appointment);
                        return true;
                    }
                }
                else if (appointment.AppointmentDateTime.DayOfWeek == System.DayOfWeek.Friday)
                {
                    if (appointment.AppointmentDateTime.TimeOfDay >= availabilityTreator.FRStartTime.TimeOfDay && appointment.EndDateTime.TimeOfDay <= availabilityTreator.FREndTime.TimeOfDay)
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

        public IEnumerable<DateTime> GetPossibleTimesOnDate(Treator treator, PatientFile patientFile, DateTime dateTime)
        {

            if(dateTime <= DateTime.Now)
            {
                return new List<DateTime>();
            }

            List<DateTime> possibleTimes = new List<DateTime>();
            Availability availabilityTreator = availabilityRepository.GetAvailabilityForTreator(treator);
            DateTime startTime = DateTime.MinValue;
            DateTime endTime = DateTime.MinValue;
            switch (dateTime.DayOfWeek)
            {
                case System.DayOfWeek.Monday:
                    startTime = availabilityTreator.MOStartTime;
                    endTime = availabilityTreator.MOEndTime;
                    break;
                case System.DayOfWeek.Tuesday:
                    startTime = availabilityTreator.TUStartTime;
                    endTime = availabilityTreator.TUEndTime;
                    break;
                case System.DayOfWeek.Wednesday:
                    startTime = availabilityTreator.WEStartTime;
                    endTime = availabilityTreator.WEEndTime;
                    break;
                case System.DayOfWeek.Thursday:
                    startTime = availabilityTreator.THStartTime;
                    endTime = availabilityTreator.THEndTime;
                    break;
                case System.DayOfWeek.Friday:
                    startTime = availabilityTreator.FRStartTime;
                    endTime = availabilityTreator.FREndTime;
                    break;
                case System.DayOfWeek.Saturday:
                    return possibleTimes;
                case System.DayOfWeek.Sunday:
                    return possibleTimes;
            }

            DateTime currentDateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, startTime.Hour, startTime.Minute, 0);
            endTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, endTime.Hour, endTime.Minute, 0);
            
            while(currentDateTime < endTime)
            {
                if(IsPossibleTime(treator, currentDateTime, patientFile.TreatmentPlan.MinutesPerSession))
                {
                    possibleTimes.Add(currentDateTime);
                }
                currentDateTime = currentDateTime.AddMinutes(15);
            }

            return possibleTimes;
        }


        private bool IsPossibleTime(Treator treator, DateTime date, int duration)
        {
            Availability availabilityTreator = availabilityRepository.GetAvailabilityForTreator(treator);
            List<Appointment> appointmentsOnDay = appointmentRepository.GetAppointmentsForDateForTreator(treator, date);
            bool IsAvailable = true;
            foreach (Appointment a in appointmentsOnDay)
            {
                if (!(a.AppointmentDateTime.TimeOfDay > date.AddMinutes(duration).TimeOfDay || a.EndDateTime.TimeOfDay < date.TimeOfDay))
                {
                    IsAvailable = false;
                }
            }
            if (IsAvailable)
            {
                switch (date.DayOfWeek)
                {
                    case System.DayOfWeek.Monday:
                        if (date.TimeOfDay >= availabilityTreator.MOStartTime.TimeOfDay && date.AddMinutes(duration).TimeOfDay <= availabilityTreator.MOEndTime.TimeOfDay)
                        {
                            return true;
                        }
                        break;
                    case System.DayOfWeek.Tuesday:
                        if (date.TimeOfDay >= availabilityTreator.TUStartTime.TimeOfDay && date.AddMinutes(duration).TimeOfDay <= availabilityTreator.TUEndTime.TimeOfDay)
                        {
                            return true;
                        }
                        break;
                    case System.DayOfWeek.Wednesday:
                        if (date.TimeOfDay >= availabilityTreator.WEStartTime.TimeOfDay && date.AddMinutes(duration).TimeOfDay <= availabilityTreator.WEEndTime.TimeOfDay)
                        {
                            return true;
                        }
                        break;
                    case System.DayOfWeek.Thursday:
                        if (date.TimeOfDay >= availabilityTreator.THStartTime.TimeOfDay && date.AddMinutes(duration).TimeOfDay <= availabilityTreator.THEndTime.TimeOfDay)
                        {
                            return true;
                        }
                        break;
                    case System.DayOfWeek.Friday:
                        if (date.TimeOfDay >= availabilityTreator.FRStartTime.TimeOfDay && date.AddMinutes(duration).TimeOfDay <= availabilityTreator.FREndTime.TimeOfDay)
                        {
                            return true;
                        }
                        break;
                    case System.DayOfWeek.Saturday:
                        return false;
                    case System.DayOfWeek.Sunday:
                        return false;
                }
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
