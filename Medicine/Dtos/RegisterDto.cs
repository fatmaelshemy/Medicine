using System.ComponentModel.DataAnnotations;

namespace Medicine.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "UserName Is required")]
        public string UserName { get; set; } = default!;
        [Required(ErrorMessage = "Email Is required")]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "Password Is required")]
        public string Password { get; set; } = default!;

        public string Role { get; set; }
    }
}
