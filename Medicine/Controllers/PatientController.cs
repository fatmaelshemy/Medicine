using AutoMapper;
using Medicine.Dtos.Patient;
using Medicine.Models;
using Medicine.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace patient.Controllers
{
    public class PatientController : Controller
    {
        private readonly Ipatient _repository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientController(Ipatient repository, IMapper mapper, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("PatientsAfterEdit")]
        public async Task<IActionResult> Edit(int id, PatientDto model)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Update ApplicationUser properties
                var user = await _userManager.FindByIdAsync(model.userId);
                if (user != null)
                {
                    user.PhoneNumber = model.User.PhoneNumber;
                    user.UserName = model.User.UserName;
                    user.Email = model.User.Email;
                    user.Gender = model.User.Gender;

                    var userMange =await _userManager.UpdateAsync(user);
                }
                else
                {
                    return NotFound(" user not found");
                }
                await _context.SaveChangesAsync();
                // Update patient properties
                model.User = null;
                var map = _mapper.Map<Patient>(model);

                var e = _context.Update(map);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok("Patient updated successfully!");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
























    //if (ModelState.IsValid)
    //{



    //var map=_mapper.Map<Patient>(model);

    //    _reposatery.Update(map);

    //    return Ok("Patient updated successfully!");
    //}
    //else 
    //{
    //    return BadRequest("Patient not updated");
    // }