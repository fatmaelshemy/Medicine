namespace Medicine.Models
{
	public class TimeSlots
	{
		public int Id { get; set; }
		public string Day { get; set; }
		public TimeOnly Form { get; set; }
		public TimeOnly To { get; set; }


	}
}
