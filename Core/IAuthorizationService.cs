using Domain.Enums;

namespace Core
{
    public interface IAuthorizationService
    {
        bool IsInRole(ERole role);
    }
}