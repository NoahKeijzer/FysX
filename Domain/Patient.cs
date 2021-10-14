using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Student { get; set; }
        public int RegistrationNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public byte[] Image { get; set; }

        ICollection<Appointment> Appointments { get; set; }
        PatientFile PatientFile { get; set; }

        public Patient(int id, string name, string email, string phoneNumber, bool student, int registrationNumber, DateTime birthDate, Gender gender, ICollection<Appointment> appointments, PatientFile patientFile)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Student = student;
            RegistrationNumber = registrationNumber;
            BirthDate = birthDate;
            Gender = gender;
            Appointments = appointments;
            PatientFile = patientFile;
        }

        public Patient()
        {

        }
    }
}
