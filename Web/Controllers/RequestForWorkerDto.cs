using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Web.Controllers
{
    public class RequestForWorkerDto
    {
        public long Id { get; set; }
        public EState State { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
        public string ConsumerName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public string Phone { get; set; }

        [Required]
        public EState? NextState { get; set; } 

        public IReadOnlyList<EState> NextStates { get; set; }

        [DataType(DataType.MultilineText)]
        public string WorkerComment { get; set; }
    }
}