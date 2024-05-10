
using Medicine.Models;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Repozitorys
{
       public class RepoSpecialization : ISpecialization
        {
        private readonly ApplicationDbContext _dbContext;

        public RepoSpecialization(ApplicationDbContext context)
        {
            _dbContext = context;
        }
       
            public List<Specialization> GetAll()
            {
                return _dbContext.Set<Specialization>().ToList();  
            }

        //public Specialization GetById(int id)
        //{

        //    return _dbContext.Set<Specialization>().FirstOrDefault(e =>e.Id=id);

        //}
        public void Insert(Specialization specialization)
            {
                if (specialization == null)
                {
                    throw new ArgumentNullException(nameof(specialization));
                }

            _dbContext.Add(specialization);

            }

            public void Save()
            {
            _dbContext.SaveChanges();
            }

        }

    }
