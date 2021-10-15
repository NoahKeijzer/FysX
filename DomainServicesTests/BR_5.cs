using Domain;
using Fysio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DomainServicesTests
{
    public class BR_5
    {
        //De leeftijd van een patiënt is >= 16. 

        [Theory]
        [InlineData(14, false)]
        [InlineData(15, false)]
        [InlineData(16, true)]
        [InlineData(17, true)]
        [InlineData(18, true)]
        public void AddPatientCheckOnAge(int years, bool shouldPass)
        {
            //Arrange
            DateTime birthday = DateTime.Now.AddYears(-(years));
            PatientModel sut = new PatientModel() { Birthdate = birthday, Email = "email@test.com", Gender = true, Name = "Test Persoon", PhoneNumber = "061234567", Teacher = true, RegistrationNumber = 1234567  };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(sut, null, null);


            //Act
            bool result = Validator.TryValidateObject(sut, context, results);

            //Assert
            Assert.Equal(result, shouldPass);
        }
    }
}
