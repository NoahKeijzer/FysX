using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DomainServices.Interfaces
{
    public interface IAvailabilityRepository
    {
        public void AddAvailability(Availability availability);
        public Availability GetAvailabilityForTreator(Treator treator);
        public void UpdateAvailability(Treator treator, Availability updatedAvailability);
        public void DeleteAvailability(Availability availability);
        public List<Availability> GetAllAvailabilities();
    }
}
