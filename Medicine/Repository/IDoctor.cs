using Medicine.Dtos;
using Medicine.Models;

namespace Medicine.Repository
{
    public interface IDoctor
    {
        public List<SpecilizationDto> GetAll();


        public Doctor GetById(int id);

        public void Insert(Doctor obj);

        public void Update(Doctor obj);

        public void Delete(int id);

        public void Save();
        public void UpdateQualifications(ICollection<Qualifications> existingQualifications, List<QualificationDto> updatedQualifications);
        public void UpdateExperiences(ICollection<Experience> existingExperiences, List<ExperienceDto> updatedExperiences);
        public void UpdateTimeSlot(ICollection<TimeSlots> existingTimeSlot, List<TimeSlotDto> updatedTimeSlot);


    }
}
