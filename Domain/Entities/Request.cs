using System;
using Domain.Enums;

namespace Domain.Entities
{
    public class Request : PersistingObject
    {
        public virtual string ConsumerName { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Comment { get; set; }

        public virtual string WorkerComment { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual Category Category { get; set; }

        public virtual EState State { get; set; }

        public virtual User Author { get; set; }

        public virtual User Executor { get; set; }
    }
}