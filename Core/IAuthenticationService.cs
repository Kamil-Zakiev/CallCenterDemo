using System.Security.Principal;
using System.Web;
using Domain.Entities;

namespace Core
{
    public interface IAuthenticationService
    {
        HttpContext HttpContext { get; set; }

        IPrincipal CurrentUserPrincipal { get; }

        User Login(LoginPassDto loginPasswordDto);

        void LogOut();
    }
}