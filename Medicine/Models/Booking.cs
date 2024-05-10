using System.ComponentModel.DataAnnotations.Schema;

namespace Medicine.Models
{
	public class Booking
	{
		public int Id { get; set; }

		[ForeignKey("Specialization")]
		public int Specialization_id { get; set; }
		public Specialization? Specialization { get; set; }

		[ForeignKey("doctor")]
		public int doctor_id { get; set; }
		public Doctor? doctor { get; set; }

		public string Name { get; set; }


		[ForeignKey("patient")]
		public int Patient_id { get; set; }
		public Patient? patient { get; set; }


		[ForeignKey("TimeSlots")]
		public int DateTime { get; set; }
		public TimeSlots? TimeSlots { get; set; }


	}
}
