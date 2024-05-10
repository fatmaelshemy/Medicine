using Medicine.Dtos;
using Medicine.Dtos.Patient;
using Medicine.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Repository
{
    public class PatientRepo : Ipatient
    {
        private readonly ApplicationDbContext _dbContext;
        public PatientRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PatientDto> getPatientById(string id)
        {
            List<PatientDto> patient =
                 _dbContext.Patients
                 .Where(p => p.userId == id).Include(p => p.User)
                 .Select(p => new PatientDto()
                 {
                     UsreName = p.User.UserName,
                     Email = p.User.Email,
                     BloodType = p.BloodType,
                     PhoneNumber = p.User.PhoneNumber,
                     Id = p.Id,
                     ImageUrl = p.User.ImageUrl,
                 }).ToList();

            return patient;

        }

        public void UpdateProfile(string id, Patient patient)
        {
            Patient paitent = _dbContext.Patients.FirstOrDefault(p => p.userId == id);
            if (patient != null)
            {
                _dbContext.Patients.Update(patient);
                _dbContext.SaveChanges();

            }
            else
            {
                _dbContext.Patients.Add(patient);
                _dbContext.SaveChanges();

            }

        }


    }
}
