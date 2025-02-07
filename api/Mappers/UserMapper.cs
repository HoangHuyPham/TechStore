using api.DTOs.Auth;
using api.DTOs.User;
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

        public static UserDTO ParseToDTO(this User user){
            return new UserDTO{
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                Avatar = user.Avatar,
                Gender = user.Gender,
                Role = user.Role,
                Review = user.Review,
                Orders = user.Orders,
                CreatedOn = user.CreatedOn,
            };
        }
    }
}