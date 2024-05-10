namespace Medicine.Dtos
{
    public class UpdateDoctorDto
    {
        public BasicDoctorData BasicInfo { get; set; }
        public int SpecializationId { get; set; }

        public List<QualificationDto> Qualifications { get; set; }
        public List<ExperienceDto> Experiences { get; set; }
        public List<TimeSlotDto> TimeSlots { get; set; }
    }
}
