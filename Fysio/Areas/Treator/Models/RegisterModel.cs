using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Areas.Treator.Models
{
    public class RegisterModel : IValidatableObject
    {
        public string Username { get; set; }
        [Required(ErrorMessage = "Email moet ingevuld worden")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Wachtwoord moet ingevuld worden")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Wachtwoord moet ingevuld worden")]
        public string ConfirmPassword { get; set; }

        public RegisterModel(string email, string password, string confirmPassword)
        {
            this.Username = email.Split("@")[0];
            this.Email = email;
            this.Password = password;
            this.ConfirmPassword = confirmPassword;
        }

        public RegisterModel()
        {

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Password.Equals(ConfirmPassword))
            {
                yield return new ValidationResult("Wachtwoorden moeten overeen komen");
            }
        }
    }
}
