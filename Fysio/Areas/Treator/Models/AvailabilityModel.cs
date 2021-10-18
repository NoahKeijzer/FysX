using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Areas.Treator.Models
{
    public class AvailabilityModel : IValidatableObject
    {
        [Required(ErrorMessage = "Maandag begintijd is leeg")]
        public DateTime MOStartTime { get; set; }
        [Required(ErrorMessage = "Maandag eindtijd is leeg") ]
        public DateTime MOEndTime { get; set; }
        [Required(ErrorMessage = "Dinsdag begintijd is leeg")]
        public DateTime TUStartTime { get; set; }
        [Required(ErrorMessage = "Dinsdag eindtijd is leeg")]
        public DateTime TUEndTime { get; set; }
        [Required(ErrorMessage = "Woensdag begintijd is leeg")]
        public DateTime WEStartTime { get; set; }
        [Required(ErrorMessage = "Woensdag eindtijd is leeg")]
        public DateTime WEEndTime { get; set; }
        [Required(ErrorMessage = "Donderdag begintijd is leeg")]
        public DateTime THStartTime { get; set; }
        [Required(ErrorMessage = "Donderdag eindtijd is leeg")]
        public DateTime THEndTime { get; set; }
        [Required(ErrorMessage = "Vrijdag begintijd is leeg")]
        public DateTime FRStartTime { get; set; }
        [Required(ErrorMessage = "Vrijdag eindtijd is leeg")]
        public DateTime FREndTime { get; set; }

        public AvailabilityModel()
        {

        }

        public AvailabilityModel(DateTime mOStartTime, DateTime mOEndTime, DateTime tUStartTime, DateTime tUEndTime, DateTime wEStartTime, DateTime wEEndTime, DateTime tHStartTime, DateTime tHEndTime, DateTime fRStartTime, DateTime fREndTime)
        {
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(MOStartTime.TimeOfDay > MOEndTime.TimeOfDay)
            {
                yield return new ValidationResult("Maandag eindtijd is eerder dan begintijd");
            }
            if(TUStartTime.TimeOfDay > TUEndTime.TimeOfDay)
            {
                yield return new ValidationResult("Dinsdag eindtijd is eerder dan begintijd");
            }
            if(WEStartTime.TimeOfDay > WEEndTime.TimeOfDay)
            {
                yield return new ValidationResult("Woensdag eindtijd is eerder dan begintijd");
            }
            if(THStartTime.TimeOfDay > THEndTime.TimeOfDay)
            {
                yield return new ValidationResult("Donderdag eindtijd is eerder dan begintijd");
            }
            if(FRStartTime.TimeOfDay > FREndTime.TimeOfDay)
            {
                yield return new ValidationResult("Vrijdag eindtijd is eerder dan begintijd");
            }
        }
    }
}
