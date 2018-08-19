using Domain.Entities;

namespace NHibernateConfigs.Maps
{
    public class UserMap : PersistingObjectMap<User>
    {
        public UserMap()
        {
            Property(user => user.Login, m => m.Column("login"));
            Property(user => user.PasswordHash, m => m.Column("pass_hash"));
            Property(user => user.Name, m => m.Column("name"));
            Property(user => user.Role, m => m.Column("role"));
        }
    }
}