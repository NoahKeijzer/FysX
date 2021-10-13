using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DomainServices.Interfaces;
using EFInfrastructure;

namespace EFInfrastructure
{
    public class DBAvailabilityRepository : IAvailabilityRepository
    {
        private readonly FysioDbContext _context;
        public DBAvailabilityRepository(FysioDbContext context)
        {
            _context = context;
        }

        public void AddAvailability(Availability availability)
        {
            if(_context.Availabilities.Where(p => p.Treator == availability.Treator).Count() == 0)
            {
                _context.Add(availability);
                _context.SaveChanges();
            } else
            {
                UpdateAvailability(availability.Treator, availability);
            }
        }

        public void DeleteAvailability(Availability availability)
        {
            _context.Remove(availability);
            _context.SaveChanges();
        }

        public List<Availability> GetAllAvailabilities()
        {
            return _context.Availabilities.ToList();
        }

        public Availability GetAvailabilityForTreator(Treator treator)
        {
            return _context.Availabilities.Where(p => p.Treator == treator).FirstOrDefault();

        }

        public void UpdateAvailability(Treator treator, Availability updatedAvailability)
        {
            Availability availability = _context.Availabilities.Where(p => p.Treator == treator).FirstOrDefault();
            availability.MOStartTime = updatedAvailability.MOStartTime;
            availability.MOEndTime = updatedAvailability.MOEndTime;
            availability.TUStartTime = updatedAvailability.TUStartTime;
            availability.TUEndTime = updatedAvailability.TUEndTime;
            availability.WEStartTime = updatedAvailability.WEStartTime;
            availability.WEEndTime = updatedAvailability.WEEndTime;
            availability.THStartTime = updatedAvailability.THStartTime;
            availability.THEndTime = updatedAvailability.THEndTime;
            availability.FRStartTime = updatedAvailability.FRStartTime;
            availability.FREndTime = updatedAvailability.FREndTime;
            _context.SaveChanges();
        }
    }
}
