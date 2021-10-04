using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DomainServices.Interfaces
{
    public interface ITreatorRepository
    {
        public void AddTreator(Treator treator);
        public void DeleteTreator(Treator treator);
        public void UpdateTreator(int id, Treator updatedTreator);
        public Treator GetTreatorByEmail(string email);
        public List<Treator> GetAllTreators();
        public List<FysioTherapist> GetAllFysios();
        public List<Student> GetAllStudents();
    }
}
