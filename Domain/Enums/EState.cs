using Domain.Attributes;

namespace Domain.Enums
{
    public enum EState
    {
        [Display(Name = "Зарегистрирована")]
        Registered = 1,

        [Display(Name = "В работе")]
        InProgress = 2,

        [Display(Name = "Выполнено")]
        Done = 3,

        [Display(Name = "Не будет выполнено")]
        NotDone = 4
    }
}