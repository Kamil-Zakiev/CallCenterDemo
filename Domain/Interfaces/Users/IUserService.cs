using System.Collections.Generic;
using Domain.Interfaces.Users.Dto;

namespace Domain.Interfaces.Users
{
    public interface IUserService
    {
        CreateUserResult CreateNewUser(NewUserDto newUserDto);

        IReadOnlyList<UserDto> GetUsers();
    }
}
