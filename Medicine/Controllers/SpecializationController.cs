
using Medicine.Models;
using Medicine.Repozitorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Windows.Input;


namespace Medicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        RepoSpecialization RepoSpecialization;

        ISpecialization _specialization1;
        public SpecializationController(ISpecialization specialization1)
        {
            _specialization1 = specialization1;
        }

       

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Specialization> DepList = _specialization1.GetAll();
            return Ok(DepList);
           
        }

        //[HttpGet("{id:int}")]
        //public IActionResult GetByID(int id)
        //{
        //    Specialization specialization = _specialization1.GetById(id);
        //    if (specialization == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(specialization);
        //}
        //api/Specialization :Post {id:1,na}
        [HttpPost]
        public IActionResult AddDept(Specialization newDept)
        {
            if (ModelState.IsValid == true)
            {
                _specialization1.Insert(newDept);
                _specialization1.Save();
                return CreatedAtAction("GetById", new { id = newDept.Id }, newDept);
            }
            return BadRequest(ModelState);
        }


    }
}
