using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Medicine.Repository
{
    public class Repo<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;

        public Repo(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Add(T model)
        {
            _context.Set<T>().Add(model);
            _context.SaveChanges();
        }

        public void Update(T model)
        {
            _context.Set<T>().Update(model);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
