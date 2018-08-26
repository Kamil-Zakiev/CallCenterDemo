using System.ComponentModel.DataAnnotations;

namespace Web.Controllers
{
    public class LoginPasswordDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = true;
    }
}