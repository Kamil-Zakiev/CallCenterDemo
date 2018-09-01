using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Interfaces.Requests.Dto
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
        [RegularExpression("[0-9]*", ErrorMessage = "Телефонный номер должен состоять из цифр")]
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Наименование категории
        /// </summary>
        [Display(Name = "Наименование категории")]
        [HiddenInput]
        public string CategoryName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public long CategoryId { get; set; }

        /// <summary>
        ///     Комментарий к заявке
        /// </summary>
        [Display(Name = "Комментарий к заявке")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        [HiddenInput(DisplayValue = false)]
        public long AuthorId { get; set; }
    }
}