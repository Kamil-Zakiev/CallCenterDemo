using Core;
using Domain.Enums;
using Domain.Interfaces.Users;

namespace Web.Autentications
{
    public class AuthorizationService : IAuthorizationService
    {
        public ICurrentOperatorService CurrentOperatorService { get; set; }

        public bool IsInRole(ERole role)
        {
            var currentUser = CurrentOperatorService.GetCurrentUser();
            return currentUser != null && currentUser.Role == role;
        }
    }
}