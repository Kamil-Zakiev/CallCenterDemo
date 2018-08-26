using System;
using System.Web;

namespace Web.Autentications
{
    public class AuthHttpModule : IHttpModule
    {
        public static CustomAutentication BadDesicionAutentication;

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += Authenticate;
        }

        public void Dispose()
        {
        }

        private void Authenticate(object source, EventArgs e)
        {
            var app = (HttpApplication) source;
            var context = app.Context;

            BadDesicionAutentication = new CustomAutentication
            {
                HttpContext = context
            };

            context.User = BadDesicionAutentication.CurrentUserPrincipal;
        }
    }
}