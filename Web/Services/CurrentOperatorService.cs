using System.Linq;
using Domain.Entities;
using Domain.Enums;
using NHibernateConfigs;

namespace Web.Services
{
    public class CurrentOperatorService
    {
        public User GetCurrentUser()
        {
            var userDataStore = new DataStore<User>();
            var firstUser = userDataStore.GetAll().FirstOrDefault();
            if (firstUser == null)
            {
                firstUser = new User()
                {
                    Login = "test user",
                    Name = "Test",
                    PasswordHash = "123",
                    Role = ERole.Admin
                };
                userDataStore.Save(firstUser);
            }
            return firstUser;
        }
    }
}