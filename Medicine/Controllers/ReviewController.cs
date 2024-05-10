using Medicine.Dtos;
using Medicine.Models;
using Medicine.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReview _reviewRepo;

        public ReviewController(IReview reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }
        [HttpPost]
        public async Task<ActionResult<Review>> CreateReview(ReviewDto reviewDto)
        {
            var review = new Review
            {
                Num_Satrt = reviewDto.Num_Stars,
                Message = reviewDto.Message,
                doctor_id = reviewDto.DoctorId,
                Patient_id = reviewDto.PatientId
            };

            var createdReview = await _reviewRepo.CreateNewReview(review);
            return Ok("تم اضافة الريفيو بنجاح");
        }
        [HttpGet("GetByReview/{id}")]
        public ActionResult<ReviewDto> GetReviewById(int id)
        {
            var review = _reviewRepo.GetReviewById(id);
            if (review == null)
            {
                return NotFound();
            }
            var reviewDto = new ReviewDto
            {
                Num_Stars = review.Num_Satrt,
                Message = review.Message,
                DoctorId = review.doctor_id,
                PatientId = review.Patient_id
            };
            return Ok(reviewDto) ;
        }

        [HttpGet("GetByDoctor/{doctorId}")]
        public ActionResult<List<ReviewDto>> GetReviewsByDoctor(int doctorId)
        {
            var reviews = _reviewRepo.GetReviewsByDoctorId(doctorId);
            var reviewsList = reviews.Select(r => new ReviewDto
            {
                Num_Stars = r.Num_Satrt,
                Message = r.Message,
                DoctorId = r.doctor_id,
                PatientId = r.Patient_id
            }).ToList();
            return Ok(reviewsList);
        }

        [HttpGet("GetByPatient/{patientId}")]
        public ActionResult<List<ReviewDto>> GetReviewsByPatient(int patientId)
        {
            var reviews = _reviewRepo.GetReviewsByPatientId(patientId);
            var reviewsList = reviews.Select(r => new ReviewDto
            {
                Num_Stars = r.Num_Satrt,
                Message = r.Message,
                DoctorId = r.doctor_id,
                PatientId = r.Patient_id
            }).ToList();

            return Ok(reviewsList);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewDto reviewDto)
        {
            var reviewToUpdate = _reviewRepo.GetReviewById(id);
            if (reviewToUpdate == null)
            {
                return NotFound();
            }

            reviewToUpdate.Num_Satrt = reviewDto.Num_Stars;
            reviewToUpdate.Message = reviewDto.Message;
            reviewToUpdate.doctor_id = reviewDto.DoctorId;
            reviewToUpdate.Patient_id = reviewDto.PatientId;

            _reviewRepo.Update(reviewToUpdate);
            _reviewRepo.Save();

            return Ok("...تم تعديل الريفيو بنجاح");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReview(int id)
        {
            var review = _reviewRepo.GetReviewById(id);
            if (review == null)
            {
                return NotFound();
            }

            _reviewRepo.Delete(id);
            _reviewRepo.Save();

            return Ok("...تم حذف الريفيو بنجاح");
        }
    }
}
