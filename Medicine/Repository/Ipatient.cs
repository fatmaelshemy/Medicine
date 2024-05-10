using Medicine.Dtos.Patient;
using Medicine.Models;

namespace Medicine.Repository
{
    public interface Ipatient
    {
        public void UpdateProfile(string id, Patient patient);
        public List<PatientDto> getPatientById(string id);
    }
}
