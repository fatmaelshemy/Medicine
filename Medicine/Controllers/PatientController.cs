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
        IRepository<Patient> _reposatery;
         IMapper _mapper;
        public PatientController(IRepository<Patient> reposatery, IMapper mapper)
        {
            _reposatery= reposatery;
            _mapper = mapper;
        }
        [HttpGet("GetAllPatients")]
        public IActionResult Index()
        {
          var patients= _reposatery.GetAll();
            if (patients == null || patients.Count()== 0)
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
             var patient= _reposatery.GetById(id);
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
            _reposatery.Add(mapp);
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
            var removePatient = _reposatery.GetById(id);
            if (removePatient != null)
            {
                _reposatery.Delete(id);
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
            var patient = _reposatery.GetById(id);
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

                var patient = _reposatery.GetById(id);
                {
                    return NotFound("Sorry! Patient not found");
                }
            var map=_mapper.Map<Patient>(model);
                _reposatery.Update(map);
                return RedirectToAction("Index");
            }
            else 
            {
                return BadRequest("Patient not updated");
             }
        }
        
    }
}
