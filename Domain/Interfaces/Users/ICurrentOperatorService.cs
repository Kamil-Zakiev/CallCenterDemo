using Domain.Entities;

namespace Domain.Interfaces.Users
{
    public interface ICurrentOperatorService
    {
        User GetCurrentUser();
    }
}