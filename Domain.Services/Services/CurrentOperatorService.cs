using System.Linq;
using Core;
using Domain.Entities;
using Domain.Interfaces.Users;

namespace Domain.Services.Services
{
    public class CurrentOperatorService : ICurrentOperatorService
    {
        public IDataStore<User> UserDataStore { get; set; }

        public IAuthenticationService AuthenticationService { get; set; }

        public User GetCurrentUser()
        {
            var principal = AuthenticationService.CurrentUserPrincipal;
            if (!principal.Identity.IsAuthenticated)
            {
                return null;
            }

            var login = principal.Identity.Name;
            return UserDataStore.GetAll().SingleOrDefault(user => user.Login == login);
        }
    }
}