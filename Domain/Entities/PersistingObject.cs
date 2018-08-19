namespace Domain.Entities
{
    public abstract class PersistingObject
    {
        public virtual long Id { get; set; }
    }
}