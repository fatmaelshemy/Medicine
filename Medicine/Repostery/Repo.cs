using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Medicine.Repository
{
    public class Repo<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private DbSet<T> _dbSet=null;

        public Repo(DbContext context)
        {
            _context = context;
             _dbSet= _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T model)
        {
            _dbSet.Add(model);
            _context.SaveChanges();
        }

        public void Update(T model)
        {
            _dbSet.Attach(model);
           _context.Entry(model).State = EntityState.Modified;
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
