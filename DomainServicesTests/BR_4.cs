using Domain;
using DomainServices.Interfaces;
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
    public class BR_4
    {
        //Bij een aantal behandelingen is een toelichting verplicht. 

        [Fact]
        public void AddTreatmentCheckRequiredExplanationTrue()
        {
            //Arrange
            var TreatmentRepo = new Mock<ITreatmentRepository>();
            var TreatmentTypeRepo = new Mock<ITreatmentTypeRepository>();

            TreatmentType treatmentType = new TreatmentType() { RequireExplanation = true };
            Patient p = new Patient();
            Treatment t = new Treatment() { Type = "1", Patient = p, Description = "blabla" };
            string token = "token";

            TreatmentTypeRepo.Setup(p => p.GetTreatmentById("1", token)).Returns(treatmentType);

            var sut = new DBAddTreatmentService(TreatmentTypeRepo.Object, TreatmentRepo.Object);

            //Act
            var result = sut.AddTreatment(t, token);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void AddTreatmentCheckRequiredExplanationFalse()
        {
            //Arrange
            var TreatmentRepo = new Mock<ITreatmentRepository>();
            var TreatmentTypeRepo = new Mock<ITreatmentTypeRepository>();

            TreatmentType treatmentType = new TreatmentType() { RequireExplanation = true };
            Patient p = new Patient();
            Treatment t = new Treatment() { Type = "1", Patient = p, Description = null };
            string token = "token";

            TreatmentTypeRepo.Setup(p => p.GetTreatmentById("1", token)).Returns(treatmentType);

            var sut = new DBAddTreatmentService(TreatmentTypeRepo.Object, TreatmentRepo.Object);

            //Act
            var result = sut.AddTreatment(t, token);

            //Assert
            Assert.False(result);
        }
    }
}
