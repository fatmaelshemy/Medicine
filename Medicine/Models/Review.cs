using System.ComponentModel.DataAnnotations.Schema;

namespace Medicine.Models
{
	public class Review
	{
		public int Id { get; set; }
		public string Num_Satrt { get; set; }
		public string Message { get; set; }

		[ForeignKey("doctor")]
		public int doctor_id { get; set; }
		public Doctor? doctor { get; set; }

		[ForeignKey("patient")]
		public int Patient_id { get; set; }
		public Patient? patient { get; set; }
	}
}
