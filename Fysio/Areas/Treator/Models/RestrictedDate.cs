using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Areas.Treator.Models
{
    public class RestrictedDate : ValidationAttribute
    {
        public override bool IsValid(object date)
        {
            DateTime dateObject = (DateTime)date;
            return dateObject < DateTime.Now.AddYears(-16);
        }
    }
}
