﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DomainServices.Interfaces;
using EFInfrastructure;

namespace EFInfrastructure
{
    public class DBTreatorRepository : ITreatorRepository
    {
        private readonly FysioDbContext _context;
        public DBTreatorRepository(FysioDbContext context)
        {
            _context = context;
        }

        public void AddTreator(Treator treator)
        {
            _context.Add(treator);
            _context.SaveChanges();
        }

        public void DeleteTreator(Treator treator)
        {
            _context.Remove(treator);
            _context.SaveChanges();
        }

        public List<FysioTherapist> GetAllFysios()
        {
            List<Treator> AllTreators = _context.Treators.ToList();
            List<FysioTherapist> FysioTherapists = new List<FysioTherapist>();
            foreach(Treator t in AllTreators)
            {
                try
                {
                    FysioTherapists.Add((FysioTherapist)t);
                    continue;
                }
                catch
                {
                    continue;
                }
            }
            return FysioTherapists;
        }

        public List<Student> GetAllStudents()
        {
            List<Treator> AllTreators = _context.Treators.ToList();
            List<Student> Students = new List<Student>();
            foreach (Treator t in AllTreators)
            {
                try
                {
                    Students.Add((Student) t);
                    continue;
                }
                catch
                {
                    continue;
                }
            }
            return Students;
        }

        public List<Treator> GetAllTreators()
        {
            return _context.Treators.ToList();
        }

        public Treator GetTreatorByEmail(string email)
        {
            return _context.Treators.Where(p => p.Email == email).FirstOrDefault();
        }

        public void UpdateTreator(int id, Treator updatedTreator)
        {
            Treator old = _context.Treators.Where(p => p.Email == updatedTreator.Email).FirstOrDefault();
            old = updatedTreator;
            _context.SaveChanges();
        }
    }
}
