using AutoMapper;
using Medicine.Dtos.Patient;
using Medicine.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;

namespace patient.Controllers
{
    public class PatientController : Controller
    {
        ApplicationDbContext _dbcontext;
        IMapper _mapper;
        public PatientController(ApplicationDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }
        [HttpGet("GetAllPatients")]
        public IActionResult Index()
        {
          var patients= _dbcontext.Patients.Include(a=>a.User).ToList();
            if (patients == null || patients.Count == 0)
            {
                return NoContent();
            }
           var mapp= _mapper.Map<List<PatientDto>>(patients);
            return Ok(mapp);
        }
        [HttpGet("GetPatient")]
        public IActionResult GetPatient(int id)
        {
            if (id == 0)
            {
                return BadRequest("Please enter a valid id");
            }
         //  var patient = _dbcontext.Patients.Find(id);
             var patient= _dbcontext.Patients.Include(s=>s.User).FirstOrDefault(a=>a.Id==id);
            //var patient = _dbcontext.Patients.First(a=>a.Id==id);
            //var patient = _dbcontext.Patients.Where(a=>a.Id==id).First();
            if (patient==null)
            {
                return NotFound("Sorry! Patient not found");
            }
            else
            {
                var mapp = _mapper.Map<PatientDto>(patient);
                return Ok(mapp);
            }
          
        }
        [HttpPost("AddPatients")]
        public IActionResult Add([FromBody] AddPatientDto addPatientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Enter a valid Patient");
            }
            var mapp = _mapper.Map<Patient>(addPatientDto);
            _dbcontext.Patients.Add(mapp);
            _dbcontext.SaveChanges();
            return Ok("Patient added successfully");
        }
        // Remove 
        [HttpGet("RemovePatients")]
        public IActionResult Remove(int id)
        {
            if (id == 0)
            {
                return BadRequest("Please enter a valid id");
            }
            var removePatient =_dbcontext.Patients.FirstOrDefault(a=>a.Id==id);
            if (removePatient != null)
            {
                _dbcontext.Remove(removePatient);
                _dbcontext.SaveChanges();
                return Ok("Patient deleted successfully");
            }
            else
            {
                return NotFound("Sorry! Patient not found");
            }
        
        }

        // Edit 
        [HttpGet("EditPatients")]
        public IActionResult Edit(int id)
        {

            if (id == 0)
            {
                return BadRequest("Please enter a valid id");
            }
            //  var patient = _dbcontext.Patients.Find(id);
            var patient = _dbcontext.Patients.Include(s => s.User).FirstOrDefault(a => a.Id == id);
            //var patient = _dbcontext.Patients.First(a=>a.Id==id);
            //var patient = _dbcontext.Patients.Where(a=>a.Id==id).First();
            if (patient == null)
            {
                return NotFound("Sorry! Patient not found");
            }
            else
            {
                var mapp = _mapper.Map<PatientDto>(patient);
                return Ok(mapp);
            }
        }

        // handle edit 
        [HttpPost("PatientsAfterEdit")]
        public IActionResult Edit(int id, UpdatePatientDto model)
        {
            if (ModelState.IsValid)
            {
            
               var patient= _dbcontext.Patients.AsNoTracking().Include(a => a.User).FirstOrDefault(a => a.Id == id);
               if(patient == null)
                {
                    return NotFound("Sorry! Patient not found");
                }
            var map=_mapper.Map<Patient>(model);
                _dbcontext.Update(map);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            else 
            {
                return BadRequest("Patient not updated");
             }
        }
        
    }
}
