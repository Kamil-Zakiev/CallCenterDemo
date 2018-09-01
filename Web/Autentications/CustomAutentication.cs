using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Domain.Entities;
using NHibernateConfigs;
using Web.Controllers;
using Web.Services;

namespace Web.Autentications
{
    public class CustomAutentication
    {
        private const string CookieName = "__AUTH_COOKIE";

        public HttpContext HttpContext { get; set; }

        private IPrincipal _currentUser;

        public IPrincipal CurrentUserPrincipal
        {
            get
            {
                if (_currentUser == null)
                {
                    try
                    {
                        var authCookie = HttpContext.Request.Cookies.Get(CookieName);
                        if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                        {
                            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                            _currentUser = new UserProvider(ticket.Name);
                        }
                        else
                        {
                            _currentUser = new UserProvider(null);
                        }
                    }
                    catch (Exception ex)
                    {
                        _currentUser = new UserProvider(null);
                    }
                }
                return _currentUser;
            }
        }

        public object Login(LoginPasswordDto loginPasswordDto)
        {
            var userDs = new DataStore<User>();

            var hashService = new PasswordHashService();
            var passHash = hashService.GetHash(loginPasswordDto.Password);
            var user = userDs.GetAll().SingleOrDefault(u => u.Login == loginPasswordDto.Login && u.PasswordHash == passHash);

            if (user != null)
            {
                CreateCookie(user, loginPasswordDto.RememberMe);
            }

            return user;
        }

        private void CreateCookie(User user, bool isPersistent = false)
        {
            var ticket = new FormsAuthenticationTicket(
                1,
                user.Login,
                DateTime.Now,
                DateTime.Now.Add(FormsAuthentication.Timeout),
                isPersistent,
                user.Role.ToString(),
                FormsAuthentication.FormsCookiePath);

            var encTicket = FormsAuthentication.Encrypt(ticket);
            var authCookie = new HttpCookie(CookieName)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };

            HttpContext.Response.Cookies.Set(authCookie);
        }

        public void LogOut()
        {
            var httpCookie = HttpContext.Response.Cookies[CookieName];
            if (httpCookie != null)
            {
                httpCookie.Value = string.Empty;
            }
        }
    }
}