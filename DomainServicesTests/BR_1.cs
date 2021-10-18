using Domain;
using DomainServices.Interfaces;
using DomainServices.Services;
using EFInfrastructure;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace DomainServicesTests
{
    public class BR_1
    {
        //Het maximaal aantal afspraken per week wordt niet overschreden bij het boeken van een afspraak.

        [Theory]
        [InlineData(0, 2, true)]
        [InlineData(1, 2, true)]
        [InlineData(2, 2, false)]
        [InlineData(3, 2, false)]
        [InlineData(4, 6, true)]
        [InlineData(5, 6, true)]
        [InlineData(6, 6, false)]
        [InlineData(7, 6, false)]
        public void AddAppointmentCheckOnAmountOfAppointments(int amountOfAppointments, int maxAmountOfAppointments, bool shouldWork)
        {
            //Arange
            var AppointmentRepo = new Mock<IAppointmentRepository>();
            var PatientFileRepo = new Mock<IPatientFileRepository>();
            var AvailabilityRepo = new Mock<IAvailabilityRepository>();

            Treator t = new FysioTherapist();
            DateTime appointmentDateTime = DateTime.Now.AddDays(2);
            TreatmentPlan tp = new TreatmentPlan() { MinutesPerSession = 60, TreatmentsPerWeek = maxAmountOfAppointments };
            Patient patient = new Patient();
            PatientFile pf = new PatientFile() { TreatmentPlan = tp, Patient = patient };
            Appointment a = new Appointment() { Treator = t, AppointmentDateTime = appointmentDateTime };


            AppointmentRepo.Setup(p => p.GetAmountOfAppointmentsIn2Week(patient, a)).Returns(amountOfAppointments * 2);

            var sutMock = new Mock<DBAddAppointmentService>(AppointmentRepo.Object, PatientFileRepo.Object, AvailabilityRepo.Object);
            sutMock.CallBase = true;
            sutMock.Setup(p => p.IsPossibleTime(t, appointmentDateTime, 60)).Returns(true);


            //Act
            bool succes = sutMock.Object.AddAppointment(a, pf);

            //Assert
            Assert.Equal(succes, shouldWork);

        }
    }
}
