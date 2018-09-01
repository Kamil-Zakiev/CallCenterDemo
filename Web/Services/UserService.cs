using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using NHibernateConfigs;
using Web.Exceptions;
using Web.Models.Users;

namespace Web.Services
{
    public class CreateUserResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }


    public class UserService
    {
        public CreateUserResult CreateNewUser(NewUserDto newUserDto)
        {
            // todo: добавить валидацию логина

            var userDs = new DataStore<User>();

            var isLoginUsed = userDs.GetAll().Any(u => u.Login == newUserDto.Login);

            if (isLoginUsed)
            {
                return new CreateUserResult()
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

            new DataStore<User>().Save(user);

            return new CreateUserResult
            {
                Success = true
            };
        }

        public IReadOnlyList<UserDto> GetUsers()
        {
            return new DataStore<User>().GetAll()
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