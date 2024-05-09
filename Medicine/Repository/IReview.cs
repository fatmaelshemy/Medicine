using Medicine.Models;

namespace Medicine.Repository
{
    public interface IReview
    {
        Review GetReviewById(int id);
        List<Review> GetReviewsByDoctorId(int doctorId);
        List<Review> GetReviewsByPatientId(int patientId);
        Task<Review> CreateNewReview(Review review);
        void Update(Review obj);
        void Delete(int id);
        public void Save();

    }
}
