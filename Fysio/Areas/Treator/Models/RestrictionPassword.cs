using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Areas.Treator.Models
{
    public class RestrictionPassword : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            RegisterModel rm = (RegisterModel)value;
            return rm.ConfirmPassword.Equals(rm.Password) ? true : false;
        }
    }
}
