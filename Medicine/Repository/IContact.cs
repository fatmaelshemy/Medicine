using Medicine.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Medicine.Repository
{
    public interface IContact
    {
        void Insert(Contact cotact );
        void Save();


    }
}