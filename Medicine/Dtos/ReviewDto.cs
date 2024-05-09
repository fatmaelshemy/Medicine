namespace Medicine.Dtos
{
    public class ReviewDto
    {
        public string Num_Stars { get; set; }
        public string Message { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
