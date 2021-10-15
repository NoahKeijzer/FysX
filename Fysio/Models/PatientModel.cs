using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Domain;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Fysio.Models
{
    public class PatientModel : IValidatableObject
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Naam is verplicht")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Email is verplicht en mag maar een keer gebruikt worden")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Telefoonnummer is verplicht")]
        public string PhoneNumber { get; set; }

        [Required (ErrorMessage = "Het is verplicht te kiezen tussen docent of student")]
        public bool Teacher { get; set; }

        [Required (ErrorMessage = "Studentnummer of docentnummer is verplicht")]
        public int RegistrationNumber { get; set; }

        [Required(ErrorMessage = "Geboortedatum is verplicht en je moet ouder zijn dan 16")]
        [RestrictedDate]
        public DateTime Birthdate { get; set; }

        [Required (ErrorMessage = "Geslacht is verplicht")]
        public bool Gender { get; set; }

        public IFormFile Image { get; set; }

        public string ImageSrc { get; set; }


        public Patient ConvertPatientModelToPatient()
        {
            Gender gender = Gender ? Domain.Gender.Male : Domain.Gender.Female;
            return new Patient { Id = Id, BirthDate = Birthdate, Email = Email, Name = Name, PhoneNumber = PhoneNumber, RegistrationNumber = RegistrationNumber, Student = !Teacher, Gender = gender, Image =  GetByteArrayFromImage(this.Image)};
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CalculateAge(Birthdate) < 16)
            {
                yield return new ValidationResult("Patient moet minimaal 16 jaar zijn");
            }
        }

        private byte[] GetByteArrayFromImage(IFormFile file)
        {
            using var target = new MemoryStream();
            file.CopyTo(target);
            return target.ToArray();
        }


        private int CalculateAge(DateTime date)
        {
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int dob = int.Parse(date.ToString("yyyyMMdd"));
            return (now - dob) / 10000;
        }
    }
}
