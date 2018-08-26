using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Services
{
    public class PasswordHashService
    {
        public string GetHash(string password)
        {
            return password + "<test hash>";
        }
    }
}