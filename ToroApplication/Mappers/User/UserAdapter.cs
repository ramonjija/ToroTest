using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToroApplication.DTOs.Response;

namespace ToroApplication.Mappers
{
    public class UserAdapter
    {
        public UserDto Adapt(User user)
        {
            return new UserDto()
            {
                UserId = user.UserId,
                CPF = user.CPF,
                UserName = user.UserName,
            };
        }

        public LoggedUserDto Adapt(User user, string token)
        {
            return new LoggedUserDto()
            {
                CPF = user.CPF,
                UserName = user.UserName,
                Token = token,
            };
        }
    }
}
