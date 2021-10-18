using Domain;
using DomainServices.Interfaces;
using EFInfrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DomainServicesTests
{
    public class BR_3
    {
        //Een behandeling kan niet in worden gevoerd als de patiënt nog niet in de praktijk is
        //geregistreerd of nadat de behandeling is beëindigd.
        [Fact]
        public void AddTreatmentCheckIfPatientExistsFalse()
        {
            //Arrange
            var TreatmentRepo = new Mock<ITreatmentRepository>();
            var TreatmentTypeRepo = new Mock<ITreatmentTypeRepository>();

            Treatment t = new Treatment() {} ;

            var sut = new DBAddTreatmentService(TreatmentTypeRepo.Object, TreatmentRepo.Object);

            //Act
            var result = sut.AddTreatment(t);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void AddTreatmentCheckIfPatientExistsTrue()
        {
            //Arrange
            var TreatmentRepo = new Mock<ITreatmentRepository>();
            var TreatmentTypeRepo = new Mock<ITreatmentTypeRepository>();

            TreatmentType treatmentType = new TreatmentType() { RequireExplanation = false };
            Patient p = new Patient();
            Treatment t = new Treatment() { Type = "1", Patient = p };

            TreatmentTypeRepo.Setup(p => p.GetTreatmentById("1")).Returns(treatmentType);

            var sut = new DBAddTreatmentService(TreatmentTypeRepo.Object, TreatmentRepo.Object);

            //Act
            var result = sut.AddTreatment(t);

            //Assert
            Assert.True(result);
        }
    }
}
