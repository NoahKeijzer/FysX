using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Availability
    {
        [Key]
        public int Id { get; set; }
        public Treator Treator { get; set; }

        public DateTime MOStartTime { get; set; }
        public DateTime MOEndTime { get; set; }

        public DateTime TUStartTime { get; set; }
        public DateTime TUEndTime { get; set; }

        public DateTime WEStartTime { get; set; }
        public DateTime WEEndTime { get; set; }

        public DateTime THStartTime { get; set; }
        public DateTime THEndTime { get; set; }

        public DateTime FRStartTime { get; set; }
        public DateTime FREndTime { get; set; }

        public Availability(Treator treator, DateTime mOStartTime, DateTime mOEndTime, DateTime tUStartTime, DateTime tUEndTime, DateTime wEStartTime, DateTime wEEndTime, DateTime tHStartTime, DateTime tHEndTime, DateTime fRStartTime, DateTime fREndTime)
        {
            Treator = treator;
            MOStartTime = mOStartTime;
            MOEndTime = mOEndTime;
            TUStartTime = tUStartTime;
            TUEndTime = tUEndTime;
            WEStartTime = wEStartTime;
            WEEndTime = wEEndTime;
            THStartTime = tHStartTime;
            THEndTime = tHEndTime;
            FRStartTime = fRStartTime;
            FREndTime = fREndTime;
        }

        public Availability()
        {

        }
    }
}
