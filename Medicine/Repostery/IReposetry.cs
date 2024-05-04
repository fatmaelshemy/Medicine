using System.Collections.Generic;

namespace Medicine.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T model);
        void Update(T model);
        void Delete(int id);
    }
}
