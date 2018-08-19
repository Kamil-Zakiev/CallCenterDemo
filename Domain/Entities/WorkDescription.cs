namespace Domain.Entities
{
    public class WorkDescription : PersistingObject
    {
        public virtual Request Request { get; set; }

        public virtual User User { get; set; }

        public virtual string Comment { get; set; }
    }
}