using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Areas.Treator.Models
{
    public class LoginModel
    {
        [Required (ErrorMessage ="Email is leeg")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Wachtwoord is leeg")]
        public string Password { get; set; }

        public LoginModel(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public LoginModel()
        {

        }
    }
}
