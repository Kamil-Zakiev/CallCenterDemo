using System.Security.Principal;

namespace Core
{
    public class UserIndentity : IIdentity
    {
        private string _name;

        public string Name => _name;

        public string AuthenticationType => "Forms";

        private bool _isAuth;
        public bool IsAuthenticated => _isAuth;

        public void Init(string name)
        {
            _isAuth = name != null;
            _name = name;
        }
    }
}