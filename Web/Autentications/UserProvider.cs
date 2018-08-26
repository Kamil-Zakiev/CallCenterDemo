using System.Security.Principal;

namespace Web.Autentications
{
    public class UserProvider : IPrincipal
    {
        public UserProvider(string name)
        {
            userIdentity = new UserIndentity();
            userIdentity.Init(name);
        }

        private UserIndentity userIdentity { get; }

        public bool IsInRole(string role)
        {
            return userIdentity.User.Role.ToString() == role;
        }

        public IIdentity Identity => userIdentity;
    }
}