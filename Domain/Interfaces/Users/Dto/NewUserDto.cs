using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Interfaces.Users.Dto
{
    // todo: move to Web
    public class NewUserDto
    {
        [Display(Name = "Имя пользователя")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Логин пользователя")]
        [Required]
        public string Login { get; set; }

        [Display(Name = "Роль пользователя")]
        [Required]
        [UIHint("Enum")]
        public ERole Role { get; set; }
    }
}