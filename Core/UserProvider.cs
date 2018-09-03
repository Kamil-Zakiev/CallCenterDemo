using System;
using System.Linq;
using System.Security.Principal;
using Domain.Enums;

namespace Core
{
    public class UserProvider : IPrincipal
    {
        public UserProvider(string name)
        {
            UserIdentity = new UserIndentity();
            UserIdentity.Init(name);
        }

        public IAuthorizationService AuthorizationService { get; set; }

        private UserIndentity UserIdentity { get; }

        public bool IsInRole(string roleStr)
        {
            throw new NotImplementedException();
            return AuthorizationService.IsInRole(Enum.GetValues(typeof(ERole))
                .Cast<ERole>()
                .Single(role => role.ToString() == roleStr));
        }

        public IIdentity Identity => UserIdentity;
    }
}