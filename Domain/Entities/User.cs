using Domain.Enums;

namespace Domain.Entities
{
    public class User : PersistingObject
    {
        public virtual long Login { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string Name { get; set; }

        public virtual ERole Role { get; set; }
    }
}