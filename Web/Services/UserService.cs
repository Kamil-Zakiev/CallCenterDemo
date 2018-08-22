using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;
using NHibernateConfigs;
using Web.Models.Users;

namespace Web.Services
{
    public class UserService
    {
        public void CreateNewUser(NewUserDto newUserDto)
        {
            // todo: добавить валидацию логина
            var user = new User()
            {
                Login = newUserDto.Login,
                Name = newUserDto.Name,
                PasswordHash = string.Empty,
                Role = newUserDto.Role
            };
            
            new DataStore<User>().Save(user);
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