using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Interfaces.Users.Dto
{
    // todo: вынести в модуль Web
    public class UserDto
    {
        public long Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Display(Name = "Роль")]
        [System.ComponentModel.DataAnnotations.UIHint("Enum")]
        public ERole Role { get; set; }

        public bool Blocked { get; set; }
    }
}