using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class TreatmentPlan
    {
        [Key]
        public int Id { get; set; }
        public int TreatmentsPerWeek { get; set; }
        public int MinutesPerSession { get; set; }

        public TreatmentPlan(int treatmentsPerWeek, int minutesPerSession)
        {
            TreatmentsPerWeek = treatmentsPerWeek;
            MinutesPerSession = minutesPerSession;
        }

        public TreatmentPlan()
        {

        }
    }
}
