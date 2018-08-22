using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.Enums;

namespace Web.Models.Users
{
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