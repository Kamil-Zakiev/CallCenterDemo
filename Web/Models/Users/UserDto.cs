using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Web.Models.Users
{
    public class UserDto
    {
        public long Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Display(Name = "Роль")]
        [UIHint("Enum")]
        public ERole Role { get; set; }

        public bool Blocked { get; set; }
    }
}