using System.ComponentModel.DataAnnotations.Schema;

namespace Medicine.Models
{
	public class Qualifications
	{
		public int Id { get; set; }
		public DateTime St_date { get; set; }
		public DateTime En_date { get; set; }
		public string Degree { get; set; }
		public string University { get; set; }

		[ForeignKey("doctor")]
		public int doctor_id { get; set; }
		public Doctor? doctor { get; set; }

	}
}
