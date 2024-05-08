using Medicine.Dtos;
using Medicine.Models;
using Medicine.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Medicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IDoctor _Doctor;


        public DoctorController(UserManager<ApplicationUser> userManager, IDoctor doctor)
        {
            _userManager = userManager;
            _Doctor = doctor;
        }
        [HttpGet]
        public IActionResult GetALLDoctors()
        {

            var doctors = _Doctor.GetAll();
            if (doctors == null || doctors.Count == 0)
            {
                return NotFound("No doctors found.");
            }

            return Ok(doctors);
        }
        [HttpGet("{doctorId:int}")]
        public IActionResult GetDoctorById (int doctorId)
        {
            var doctor = _Doctor.GetById(doctorId);

            if (doctor == null)
            {
                return NotFound("Doctor not found.");
            }
            var doctorDto = new GetALLDoctorsDTO
            {
                Id = doctor.Id,
                BasicInfo = new BasicDoctorData
                {
                    DoctorName = doctor.User.UserName,
                    Email = doctor.User.Email,
                    Phone = doctor.User.PhoneNumber,
                    Gender = doctor.User.Gender,
                    Bio = doctor.Bio,
                    About = doctor.About,
                    TicketPrice = doctor.TicketPrice
                },
                SpecializationName = doctor.Specialization.Name,
                Qualifications = doctor.Qualifications.Select(q => new QualificationDto
                {
                    Degree = q.Degree,
                    University = q.University,
                    StartQualificationDate = q.St_date,
                    EndQualificationsDate = q.En_date
                }).ToList(),
                Experiences = doctor.Experiences.Select(e => new ExperienceDto
                {
                    Hospital = e.Hospital,
                    Position = e.Position,
                    StartExperienceDate = e.St_date,
                    EndExperienceDate = e.En_date
                }).ToList(),
                TimeSlots = doctor.TimeSlots.Select(t => new TimeSlotDto
                {
                    DayTimeSlot = t.Day,
                    Form = t.Form,
                    To = t.To
                }).ToList()
            };
            

            return Ok(doctorDto);
        }
    

        [HttpPut("{doctorId:int}")]
        public async Task<IActionResult> UpdateDoctorProfile(int doctorId, [FromBody] UpdateDoctorDto updateDoctorDto)
        {
            var doctorFromDb = _Doctor.GetById(doctorId);

            if (doctorFromDb == null)
            {
                return NotFound("الطبيب غير موجود.");
            }

            doctorFromDb.User.UserName = updateDoctorDto.BasicInfo.DoctorName;
            doctorFromDb.User.Email = updateDoctorDto.BasicInfo.Email;
            doctorFromDb.User.PhoneNumber = updateDoctorDto.BasicInfo.Phone;
            doctorFromDb.User.ImageUrl = updateDoctorDto.BasicInfo.ImageUrl;




            var userUpdateResult = await _userManager.UpdateAsync(doctorFromDb.User);
            if (!userUpdateResult.Succeeded)
            {
                return BadRequest(userUpdateResult.Errors);
            }

            if (doctorFromDb.SpecializationId != updateDoctorDto.SpecializationId)
            {
                doctorFromDb.SpecializationId = updateDoctorDto.SpecializationId;
            }

            doctorFromDb.Bio = updateDoctorDto.BasicInfo.Bio;
            doctorFromDb.About = updateDoctorDto.BasicInfo.About;
            doctorFromDb.TicketPrice = updateDoctorDto.BasicInfo.TicketPrice;

            _Doctor.UpdateQualifications(doctorFromDb.Qualifications, updateDoctorDto.Qualifications);
            _Doctor.UpdateExperiences(doctorFromDb.Experiences, updateDoctorDto.Experiences);
            _Doctor.UpdateTimeSlot(doctorFromDb.TimeSlots, updateDoctorDto.TimeSlots);
             _Doctor.Save();

            return Ok("تم تحديث ملف الطبيب بنجاح.");
        }

    }

}
