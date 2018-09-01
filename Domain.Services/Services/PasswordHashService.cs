using Domain.Interfaces.Users;

namespace Domain.Services.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        public string GetHash(string password)
        {
            return password + "<test hash>";
        }
    }
}