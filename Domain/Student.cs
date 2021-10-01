using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Student : Treator
    {
        public int StudentNumber { get; set; }
        public Student(string Name, string Email, int StudentNumber): base(Name, Email)
        {
            this.StudentNumber = StudentNumber;
        }

        public Student() : base()
        {

        }
    }
}
