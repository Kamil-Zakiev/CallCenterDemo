using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Web.Models.Requests
{
    public class RequestDetailsDto
    {
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }

        public string State { get; set; }

        public string AuthorName { get; set; }

        public string CategoryName { get; set; }

        public string ConsumerName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public string Phone { get; set; }
    }
}