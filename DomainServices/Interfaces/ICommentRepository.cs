using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DomainServices.Interfaces
{
    public interface ICommentRepository
    {
        public void AddComment(Comment comment);
        public List<Comment> GetCommentsForPatientFile(PatientFile patientFile);
        public List<Comment> GetCommentsVisibleForPatientFromPatientFile(PatientFile patientFile);

    }
}
