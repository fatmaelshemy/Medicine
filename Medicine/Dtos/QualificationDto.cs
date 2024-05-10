namespace Medicine.Dtos
{
    public class QualificationDto
    {
        public int Id { get; set; }

        public DateTime StartQualificationDate { get; set; }
        public DateTime EndQualificationsDate { get; set; }
        public string Degree { get; set; }
        public string University { get; set; }
    }
}
