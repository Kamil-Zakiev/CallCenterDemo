using System.Linq;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Users;
using NHibernateConfigs;

namespace Domain.Services.Services
{
    public class CurrentOperatorService : ICurrentOperatorService
    {
        public IDataStore<User> UserDataStore { get; set; }

        public User GetCurrentUser()
        {
            var firstUser = UserDataStore.GetAll().FirstOrDefault();
            if (firstUser == null)
            {
                firstUser = new User()
                {
                    Login = "test user",
                    Name = "Test",
                    PasswordHash = "123",
                    Role = ERole.Admin
                };
                UserDataStore.Save(firstUser);
            }

            return firstUser;
        }
    }
}