using DomainServices.Interfaces;
using DomainServices.Services;
using Fysio.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Fysio.Models;
using Domain;
using EFInfrastructure;

namespace DomainServicesTests
{
    public class BR_2
    {
        //Afspraken kunnen alleen worden gemaakt op beschikbare momenten van de
        //hoofdbehandelaar.Hierbij moet rekening gehouden worden met de algemene
        //beschikbaarheid en de reeds gemaakte afspraken.

        [Theory]
        [InlineData("10-10-2022 08:00", false)]
        [InlineData("10-10-2022 08:59", false)]
        [InlineData("10-10-2022 09:00", true)]
        [InlineData("10-10-2022 10:00", true)]
        [InlineData("10-10-2022 16:00", true)]
        [InlineData("10-10-2022 16:01", false)]
        [InlineData("10-10-2022 17:00", false)]
        public void AddAppointmentCheckOnAvailability(string date, bool shouldWork)
        {
            //Arange
            var AppointmentRepo = new Mock<IAppointmentRepository>();
            var PatientFileRepo = new Mock<IPatientFileRepository>();
            var AvailabilityRepo = new Mock<IAvailabilityRepository>();

            Treator t = new FysioTherapist();
            DateTime appointmentDateTime = DateTime.Parse(date);
            TreatmentPlan tp = new TreatmentPlan() { MinutesPerSession = 60 };
            PatientFile pf = new PatientFile() { TreatmentPlan = tp };
            Appointment a = new Appointment() { Treator = t, AppointmentDateTime = appointmentDateTime};

            DateTime startTime = DateTime.Parse("10-10-2021 09:00AM");
            DateTime endTime = DateTime.Parse("10-10-2021 5:00PM");

            Availability availability = new Availability(t, startTime, endTime, startTime, endTime, startTime, endTime, startTime, endTime, startTime, endTime);

            AvailabilityRepo.Setup(p => p.GetAvailabilityForTreator(t)).Returns(availability);
            AppointmentRepo.Setup(p => p.GetAppointmentsForDateForTreator(t, appointmentDateTime)).Returns(new List<Appointment>());

            AddAppointmentService sut = new DBAddAppointmentService(AppointmentRepo.Object, PatientFileRepo.Object, AvailabilityRepo.Object);


            //Act
            bool succes = sut.AddAppointment(a, pf);

            //Assert
            Assert.Equal(succes, shouldWork);

        }


        [Theory]
        [InlineData("10-10-2022 10:00", false)]
        [InlineData("10-10-2022 16:00", false)]
        public void AddAppointmentCheckOnDoubleAppointment(string date, bool shouldWork)
        {
            //Arange
            var AppointmentRepo = new Mock<IAppointmentRepository>();
            var PatientFileRepo = new Mock<IPatientFileRepository>();
            var AvailabilityRepo = new Mock<IAvailabilityRepository>();

            Treator t = new FysioTherapist();
            DateTime appointmentDateTime = DateTime.Parse(date);
            TreatmentPlan tp = new TreatmentPlan() { MinutesPerSession = 60 };
            PatientFile pf = new PatientFile() { TreatmentPlan = tp };
            Appointment a = new Appointment() { Treator = t, AppointmentDateTime = appointmentDateTime, EndDateTime = appointmentDateTime.AddMinutes(tp.MinutesPerSession)};

            DateTime startTime = DateTime.Parse("10-10-2021 09:00AM");
            DateTime endTime = DateTime.Parse("10-10-2021 5:00PM");

            Availability availability = new Availability(t, startTime, endTime, startTime, endTime, startTime, endTime, startTime, endTime, startTime, endTime);

            AvailabilityRepo.Setup(p => p.GetAvailabilityForTreator(t)).Returns(availability);
            AppointmentRepo.Setup(p => p.GetAppointmentsForDateForTreator(t, appointmentDateTime)).Returns(new List<Appointment>() { a });

            AddAppointmentService sut = new DBAddAppointmentService(AppointmentRepo.Object, PatientFileRepo.Object, AvailabilityRepo.Object);


            //Act
            bool succes = sut.AddAppointment(a, pf);

            //Assert
            Assert.Equal(succes, shouldWork);

        }
    }
}
