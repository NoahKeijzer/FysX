using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Areas.Treator.Models
{
    public class AccountModel
    {
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public int SchoolNumber { get; set; }
        public int BigNumber { get; set; }

        public AccountModel(string name, string phoneNumber, int schoolNumber, int bigNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            SchoolNumber = schoolNumber;
            BigNumber = bigNumber;
        }

        public AccountModel()
        {

        }
    }
}
