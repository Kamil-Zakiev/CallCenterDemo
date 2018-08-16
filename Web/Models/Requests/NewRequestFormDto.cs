using System.ComponentModel.DataAnnotations;

namespace Web.Models.Requests
{
    /// <summary>
    ///     Форма добавления новой заявки
    /// </summary>
    public class NewRequestFormDto
    {
        /// <summary>
        ///     ФИО пользователя, от которого пришла заявка
        /// </summary>
        [Display(Name = "ФИО пользователя")]
        [Required]
        public string CustemerFio { get; set; }

        /// <summary>
        ///     Телефон пользователя
        /// </summary>
        [Display(Name = "Телефон пользователя")]
        [Required]
        // todo: [RegularExpression("[0-9]*")]
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Наименование категории
        /// </summary>
        [Display(Name = "Наименование категории")]
        [Required]
        public string CategoryName { get; set; }

        /// <summary>
        ///     Комментарий к заявке
        /// </summary>
        [Display(Name = "Комментарий к заявке")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
    }
}