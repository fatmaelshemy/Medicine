namespace Medicine.Dtos
{
    public class ExperienceDto
    {
        public int Id { get; set; }

        public DateOnly StartExperienceDate { get; set; }
        public DateOnly EndExperienceDate { get; set; }
        public string Position { get; set; }
        public string Hospital { get; set; }
    }
}
