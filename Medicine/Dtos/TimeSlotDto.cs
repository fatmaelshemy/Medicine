namespace Medicine.Dtos
{
    public class TimeSlotDto
    {
        public int Id { get; set; }

        public string DayTimeSlot { get; set; }
        public TimeOnly Form { get; set; }
        public TimeOnly To { get; set; }
    }
}
