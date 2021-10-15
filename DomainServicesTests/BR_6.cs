using Domain;
using DomainServices.Interfaces;
using DomainServices.Services;
using EFInfrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DomainServicesTests
{
    public class BR_6
    {
        //Een afspraak kan niet door een patiënt worden geannuleerd minder van 24 uur
        //voorafgaand aan de afspraak.

        [Theory]
        [InlineData(21, false)]
        [InlineData(22, false)]
        [InlineData(23, false)]
        [InlineData(24, false)]
        [InlineData(25, true)]
        [InlineData(26, true)]
        [InlineData(27, true)]
        public void UpdateAppointmentCheckOnTimeBeforeAppointment(int hours, bool shouldWork)
        {
            //Arange
            var AppointmentRepo = new Mock<IAppointmentRepository>();
            var PatientFileRepo = new Mock<IPatientFileRepository>();
            var AvailabilityRepo = new Mock<IAvailabilityRepository>();

            Treator t = new FysioTherapist();
            DateTime appointmentDateTime = DateTime.Now.AddHours(hours);
            TreatmentPlan tp = new TreatmentPlan() { MinutesPerSession = 60 };
            PatientFile pf = new PatientFile() { TreatmentPlan = tp };
            Appointment a = new Appointment() { Treator = t, AppointmentDateTime = appointmentDateTime, EndDateTime = appointmentDateTime.AddMinutes(tp.MinutesPerSession) };
            Appointment originalAppointment = new Appointment() { AppointmentDateTime = DateTime.Now.AddHours(hours), EndDateTime = DateTime.Now.AddHours(hours).AddMinutes(tp.MinutesPerSession) };

            DateTime startTime = DateTime.Parse("10-10-2021 09:00AM");
            DateTime endTime = DateTime.Parse("10-10-2021 5:00PM");

            Availability availability = new Availability(t, startTime, endTime, startTime, endTime, startTime, endTime, startTime, endTime, startTime, endTime);

            AvailabilityRepo.Setup(p => p.GetAvailabilityForTreator(t)).Returns(availability);
            AppointmentRepo.Setup(p => p.GetAppointmentsForDateForTreator(t, appointmentDateTime)).Returns(new List<Appointment>());
            AppointmentRepo.Setup(p => p.GetAppointmentById(1)).Returns(originalAppointment);

            var sutMock = new Mock<DBAddAppointmentService>(AppointmentRepo.Object, PatientFileRepo.Object, AvailabilityRepo.Object);
            sutMock.CallBase = true;
            sutMock.Setup(p => p.IsPossibleTime(t, appointmentDateTime, 0)).Returns(true);

            //AddAppointmentService sut = new DBAddAppointmentService(AppointmentRepo.Object, PatientFileRepo.Object, AvailabilityRepo.Object);

            bool succes = sutMock.Object.UpdateAppointment(a, 1);
            //Act
            //bool succes = sut.UpdateAppointment(a, 1);

            //Assert
            Assert.Equal(succes, shouldWork);

        }

        [Theory]
        [InlineData(21, false)]
        [InlineData(22, false)]
        [InlineData(23, false)]
        [InlineData(24, false)]
        [InlineData(25, true)]
        [InlineData(26, true)]
        [InlineData(27, true)]
        public void DeleteAppointmentCheckOnTimeBeforeAppointment(int hours, bool shouldWork)
        {
            //Arange
            var AppointmentRepo = new Mock<IAppointmentRepository>();
            var PatientFileRepo = new Mock<IPatientFileRepository>();
            var AvailabilityRepo = new Mock<IAvailabilityRepository>();

            DateTime appointmentDateTime = DateTime.Now.AddHours(hours);
            Appointment a = new Appointment() { AppointmentDateTime = appointmentDateTime};

            AddAppointmentService sut = new DBAddAppointmentService(AppointmentRepo.Object, PatientFileRepo.Object, AvailabilityRepo.Object);

            //Act
            bool succes = sut.DeleteAppointment(a);

            //Assert
            Assert.Equal(succes, shouldWork);

        }

    }
}
