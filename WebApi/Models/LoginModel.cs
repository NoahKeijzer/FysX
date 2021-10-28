using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public LoginModel()
        {

        }
        public LoginModel(string Email, string Password)
        {
            this.Email = Email;
            this.Password = Password;
        }
    }
}
