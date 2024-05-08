using Medicine.Dtos.Patient;
using Medicine.Models;
using Medicine.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {

        private readonly Ipatient _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientController(Ipatient repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        [HttpGet("GetById/{id}")]
        public ActionResult<List<PatientDto>> GetById(string id)
        {
            return _repository.getPatientById(id);

        }

        [HttpPost("PatientsAfterEdit/{id}")]
        public async Task<IActionResult> Edit(string id, PatientDto model)
        {

            try
            {
                // Update ApplicationUser properties
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.PhoneNumber = model.PhoneNumber;
                    user.UserName = model.UsreName;
                    user.Email = model.Email;
                    user.ImageUrl = model.ImageUrl;

                    var userMange = await _userManager.UpdateAsync(user);
                    _repository.UpdateProfile(id, new Patient()
                    {
                        BloodType = model.BloodType,
                        userId = user.Id,

                    });


                    return Ok("Patient updated successfully!");

                }
                else
                {
                    return NotFound(" user not found");
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}

