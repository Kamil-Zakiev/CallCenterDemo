using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using Web.Controllers;
using Web.Models.Common;

namespace Web.Models.Requests
{
    public class RequestListDto
    {
        public IReadOnlyList<RequestListItem> RequestListItems { get; set; }
        
        [HiddenInput(DisplayValue = false)]
        public PagesInfo PagesInfo { get; set; }

        public UserLoadParams UserLoadParams { get; set; }
    }

    public class RequestListItem
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