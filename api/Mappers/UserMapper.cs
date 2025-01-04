using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Auth;
using api.Models;

namespace api.Mappers
{
    public static class UserMapper
    {
        public static User ParseToUser(this RegisterDTO registerDTO){
            return new User{
                Name = registerDTO.Name,
                Email = registerDTO.Email,
            };
        }
    }
}