using WebApi.Dtos;

namespace WebApi.Domain.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<TblUser>> GetUsers();
        Task<TblUser> GetUserByEmail(string email);
        Task<TblUser> GetUserById(int userId);
        Task<bool> CreateUser(UserDto user);
        Task<bool> UpdateUser(int userId, UserDto user);
        Task<bool> DeleteUser(int userId);
    }
}
