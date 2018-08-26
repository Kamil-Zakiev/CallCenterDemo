using System.Linq;
using System.Security.Principal;
using Domain.Entities;
using NHibernateConfigs;

namespace Web.Autentications
{
    public class UserIndentity : IIdentity
    {
        public User User { get; set; }

        public string Name => User?.Login;

        public string AuthenticationType => "Custom";

        public bool IsAuthenticated => User != null;

        public void Init(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                User = new DataStore<User>().GetAll().SingleOrDefault(u => u.Login == name);
            }
        }
    }
}