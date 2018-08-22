using System.ComponentModel;
using Domain.Attributes;

namespace Domain.Enums
{
    public enum ERole
    {
        [Display(Name = "Администратор")]
        Admin = 1,

        [Display(Name = "Оператор")]
        Operator = 2,

        [Display(Name = "Исполнитель")]
        Worker = 3
    }
}