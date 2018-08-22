using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.Requests
{
    public class RequestListDto
    {
        public long Id { get; set; }

        /// <summary>
        ///     ФИО пользователя, от которого пришла заявка
        /// </summary>
        [Display(Name = "ФИО пользователя")]
        public string CustemerFio { get; set; }
        
        /// <summary>
        ///     Наименование категории
        /// </summary>
        [Display(Name = "Наименование категории")]
        [Required]
        public string CategoryName { get; set; }


        public string Executor { get; set; }
    }
}