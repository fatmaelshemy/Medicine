using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medicine.Models
{
	public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MinLength(3)]

        [MaxLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "E mail Is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = " Message Is Required")]
        public string Message { get; set; }
	}
}
