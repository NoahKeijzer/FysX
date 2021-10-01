using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Account
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
        public Patient Patient { get; set; }

        public Account(string Email, string Password, Patient Patient)
        {
            this.Email = Email;
            this.Password = Password;
            this.Patient = Patient;
        }
        public Account()
        {

        }
    }
}
