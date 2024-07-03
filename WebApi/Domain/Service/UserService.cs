using WebApi.Data;
using WebApi.Domain.Interface;
using WebApi.Dtos;

namespace WebApi.Domain.Service
{
    public class UserService : IUserService
    {
        private readonly WebAppDbContext _context;
        private readonly IDataAccess _dataAccess;

        public UserService(WebAppDbContext context, IDataAccess dataAccess)
        {
            _context = context;
            _dataAccess = dataAccess;
        }

        public async Task<bool> CreateUser(UserDto user)
        {
            try
            {
                string query = @"insert into dbo.Tbl_user(username,password, email, role, is_active, created_at, updated_at) 
                                values(@userName, @password, @email, @role, @isActive, @createdAt, @updatedAt)";
                await _dataAccess.SaveData(query, new
                {
                    userName = user.Username,
                    password = user.Password,
                    email = user.Email,
                    role = user.Role,
                    isActive = user.IsActive,
                    createdAt = user.CreatedAt,
                    updatedAt = user.UpdatedAt
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUser(int userId)
        {
            try
            {
                string query = @"update dbo.Tbl_user 
                                set is_active = '0'
                                where user_id=@userId";
                await _dataAccess.SaveData(query, new { userId = userId });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<TblUser> GetUserByEmail(string email)
        {
            string query = @"select 
                                user_id AS UserId, 
                                username,
                                password, 
                                email,
                                role, 
                                is_active AS IsActive,
                                created_at AS CreatedAt, 
                                updated_at AS UpdatedAt  
                            from Tbl_user 
                            where email=@email
                            and is_active = '1'";
            var user = await _dataAccess.GetData<TblUser, dynamic>(query, new { email = email });

            return user.FirstOrDefault();
        }

        public async Task<TblUser> GetUserById(int userId)
        {
            string query = @"select 
                                user_id AS UserId, 
                                username,
                                password, 
                                email,
                                role, 
                                is_active AS IsActive,
                                created_at AS CreatedAt, 
                                updated_at AS UpdatedAt  
                            from Tbl_user 
                            where user_id=@userId
                            and is_active = '1'";
            var user = await _dataAccess.GetData<TblUser, dynamic>(query, new { userId = userId });

            return user.FirstOrDefault();
        }

        public async Task<IEnumerable<TblUser>> GetUsers()
        {
            string query = @"
                            SELECT 
                                user_id AS UserId, 
                                username, 
                                password, 
                                email, 
                                role, 
                                is_active AS IsActive, 
                                created_at AS CreatedAt, 
                                updated_at AS UpdatedAt 
                            FROM Tbl_user
                            where is_active = '1'";
            var users = await _dataAccess.GetData<TblUser, dynamic>(query, new { });

            return users;
        }

        public async Task<bool> UpdateUser(int userId, UserDto user)
        {
            try
            {
                string query = @"update dbo.Tbl_user set username=@userName, password=@password, email=@email, role=@role, is_active=@isActive, created_at=@createdAt, updated_at=@updatedAt
                                where user_id=@userId and is_active = '1'";
                await _dataAccess.SaveData(query, new
                {
                    userName = user.Username,
                    password = user.Password,
                    email = user.Email,
                    role = user.Role,
                    isActive = user.IsActive,
                    createdAt = user.CreatedAt,
                    updatedAt = user.UpdatedAt,
                    userId = userId
                });

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
