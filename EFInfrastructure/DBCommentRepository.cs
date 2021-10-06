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
    public class DBCommentRepository : ICommentRepository
    {
        private readonly FysioDbContext _context;
        public DBCommentRepository(FysioDbContext context)
        {
            _context = context;
        }

        public void AddComment(Comment comment)
        {
            _context.Add(comment);
            _context.SaveChanges();
        }

        public List<Comment> GetCommentsForPatientFile(PatientFile patientFile)
        {
            return _context.PatientFiles.Where(p => p.Id == patientFile.Id).FirstOrDefault().Comments.ToList();
        }

        public List<Comment> GetCommentsVisibleForPatientFromPatientFile(PatientFile patientFile)
        {
            return _context.PatientFiles.Where(p => p.Id == patientFile.Id).FirstOrDefault().Comments.Where(p => p.VisibleForPatient == true).ToList();
        }
    }
}
