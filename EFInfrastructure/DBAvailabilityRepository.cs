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
            _context.Add(availability);
            _context.SaveChanges();
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
            availability = updatedAvailability;
            _context.SaveChanges();
        }
    }
}
