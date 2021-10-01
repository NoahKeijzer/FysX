using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FysioTherapist : Treator
    {

        public string PhoneNumber { get; set; }
        public int TeacherNumber { get; set; }
        public int BIGNumber { get; set; }
        public FysioTherapist(string Name, string Email, string phoneNumber, int teacherNumber, int bIGNumber): base(Name, Email)
        {
            PhoneNumber = phoneNumber;
            TeacherNumber = teacherNumber;
            BIGNumber = bIGNumber;
        }

        public FysioTherapist() : base()
        {

        }
    }
}
