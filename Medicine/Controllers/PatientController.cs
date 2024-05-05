using AutoMapper;
using Medicine.Dtos.Patient;
using Medicine.Models;
using Medicine.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace patient.Controllers
{
    public class PatientController : Controller
    {
        Ipatient _reposatery;
         IMapper _mapper;
        public PatientController(Ipatient reposatery, IMapper mapper)
        {
            _reposatery= reposatery;
            _mapper = mapper;
        }
        

        // edit 
        [HttpPost("PatientsAfterEdit")]
        public IActionResult Edit(int id, UpdatePatientDto model)
        {
            if (ModelState.IsValid)
            {

               
               
            var map=_mapper.Map<Patient>(model);
                _reposatery.Update(map);
                return Ok("Patient updated successfully!");
            }
            else 
            {
                return BadRequest("Patient not updated");
             }
        }
        
    }
}
