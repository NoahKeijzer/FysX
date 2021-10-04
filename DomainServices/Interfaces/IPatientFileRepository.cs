using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DomainServices.Interfaces
{
    public interface IPatientFileRepository
    {
        public void AddPatientFile(PatientFile patientFile);
        public void UpdatePatientFile(int id, PatientFile updatePatientFile);
        public PatientFile GetCurrentPatientFileForPatient(Patient patient);
        public List<PatientFile> GetPatientFilesForPatient(Patient patient);
    }
}
