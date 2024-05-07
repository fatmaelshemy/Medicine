namespace Medicine.Dtos
{
    public class GetALLDoctorsDTO
    {
        public int Id { get; set; }
        public BasicDoctorData BasicInfo { get; set; }
        public string SpecializationName { get; set; }
        public List<QualificationDto> Qualifications { get; set; }
        public List<ExperienceDto> Experiences { get; set; }
        public List<TimeSlotDto> TimeSlots { get; set; }
    }
}
