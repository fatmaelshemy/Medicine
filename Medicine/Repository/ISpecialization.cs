
using Medicine.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace Medicine.Repozitorys
{
    public interface ISpecialization
    {
        public List<Specialization> GetAll();

        //public Specialization GetById(int id);

        void Insert(Specialization obj);
        void Save();
    }
}
