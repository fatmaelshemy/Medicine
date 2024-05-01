using System.ComponentModel.DataAnnotations;

namespace Medicine.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "UserName Is required")]
        public string UserName { get; set; } = default!;
        [Required(ErrorMessage = "Password Is required")]
        public string Password { get; set; } = default!;
    }
}
