using Medicine.Models;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Repository
{
    public class ReviewRepo : IReview
    {
        ApplicationDbContext context;

        public string Id { get; set; }

        public ReviewRepo(ApplicationDbContext _context)
        {

            Id = Guid.NewGuid().ToString();
            context = _context;
        }
        public Review GetReviewById(int id)
        {
            return context.Reviews
                .Include(r => r.doctor)
                .Include(r => r.patient)
                .FirstOrDefault(r => r.Id == id);
        }
        public List<Review> GetReviewsByDoctorId(int doctorId)
        {
            return context.Reviews
                .Include(r => r.doctor)
                .Include(r => r.patient)
                .Where(r => r.doctor_id == doctorId)
                .ToList();
        }
        public List<Review> GetReviewsByPatientId(int patientId)
        {
            return context.Reviews
                .Include(r => r.doctor)
                .Include(r => r.patient)
                .Where(r => r.Patient_id == patientId)
                .ToList();
        }
        
        public async Task<Review> CreateNewReview(Review review)
        {
            context.Reviews.Add(review);
            await context.SaveChangesAsync();
            return review;
        }
        public void Update(Review obj)
        {
            context.Update(obj);
        }
        public void Delete(int id)
        {
            Review review = GetReviewById(id);
            if (review != null)
            {
                context.Reviews.Remove(review);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}
