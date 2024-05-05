using Medicine.Models;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Repository
{
    public class PatientRepo : Ipatient
    {
        private readonly ApplicationDbContext _dbContext;
        public PatientRepo(ApplicationDbContext dbContext)
        {
             _dbContext=dbContext;
        }
        public void Update(Patient patient)
        {

            _dbContext.Attach(patient);
            _dbContext.Entry(patient).State = EntityState.Modified;
            _dbContext.SaveChanges();

        }
    }
}
