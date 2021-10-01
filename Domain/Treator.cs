using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public abstract class Treator
    {
        public string Name { get; set; }
        [Key]
        public string Email { get; set; }
        public Treator(string Name, string Email)
        {
            this.Name = Name;
            this.Email = Email;
        }

        public Treator()
        {

        }
    }
}
