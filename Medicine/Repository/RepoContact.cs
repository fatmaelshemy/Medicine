using Medicine.Models;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Repository
{
    public class RepoContact:IContact
    {

        private readonly ApplicationDbContext _dbContext;

     
        public RepoContact(ApplicationDbContext context)
        {
            _dbContext = context;
        }
       
        public void Insert(Contact contact)
        {
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            _dbContext.Add(contact);

        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

    }



}

