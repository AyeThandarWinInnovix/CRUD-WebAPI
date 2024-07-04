using WebApi.Dtos;

namespace WebApi.Domain.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserDto> GetUserByEmail(string email);
        Task<UserDto> GetUserById(int userId);
        Task<UserDetailDto> GetUserDetailById(int userId);
        Task<bool> CreateUser(UserDto user);
        Task<bool> UpdateUser(int userId, UserDto user);
        Task<bool> DeleteUser(int userId);
    }
}
