using System.ComponentModel.DataAnnotations.Schema;

namespace Medicine.Models
{
	public class Experience
	{
		public int Id { get; set; }

		public DateOnly St_date { get; set; }
		public DateOnly En_date { get; set; }
		public string Position { get; set; }
		public string Hospital { get; set; }

		[ForeignKey("doctor")]
		public int doctor_id { get; set; }
		public Doctor? doctor { get; set; }



	}
}

