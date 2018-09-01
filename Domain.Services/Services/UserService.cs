using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Users;
using Domain.Interfaces.Users.Dto;

namespace Domain.Services.Services
{
    public class UserService : IUserService
    {
        public IDataStore<User> UserDataStore { get; set; }

        public CreateUserResult CreateNewUser(NewUserDto newUserDto)
        {
            var isLoginUsed = UserDataStore.GetAll().Any(u => u.Login == newUserDto.Login);
            if (isLoginUsed)
            {
                return new CreateUserResult
                {
                    Success = false,
                    Message = "Логин занят"
                };
            }

            var user = new User
            {
                Login = newUserDto.Login,
                Name = newUserDto.Name,
                PasswordHash = string.Empty,
                Role = newUserDto.Role
            };

            UserDataStore.Save(user);

            return new CreateUserResult
            {
                Success = true
            };
        }

        public IReadOnlyList<UserDto> GetUsers()
        {
            return UserDataStore.GetAll()
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    Login = user.Login,
                    Name = user.Name,
                    Role = user.Role
                })
                .ToArray();
        }
    }
}