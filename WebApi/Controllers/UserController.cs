using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Interface;
using WebApi.Dtos.UserDtos;
using WebApi.ResponseModels;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("")]
        public async Task<BaseResponseModel<UserDto>> GetUsers()
        {
            var users = await _userService.GetUsers();
            return new BaseResponseModel<UserDto>(StatusCodes.Status200OK, "Success", users.ToList());
        }

        [HttpGet]
        [Route("GetDetailsById/{userId}")]
        public async Task<BaseResponseModel<UserDetailDto>> GetUsersDetailsById(int userId)
        {
            var response = await _userService.GetUserDetailById(userId);
            return response;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<BaseResponseModel<bool>> CreateUser(UserDto user)
        {
            if (!ModelState.IsValid)
                return new BaseResponseModel<bool>(StatusCodes.Status400BadRequest, "Model is not valid");

            var result = await _userService.GetUserByEmail(user.Email);
            if (result != null) return new BaseResponseModel<bool>(StatusCodes.Status400BadRequest, "User email already exist");

            user.IsActive = true;
            var res = await _userService.CreateUser(user);
            if (res)
            {
                return new BaseResponseModel<bool>(StatusCodes.Status200OK, "Success", true);
            }
            else
            {
                return new BaseResponseModel<bool>(StatusCodes.Status500InternalServerError, "Error creating user", false);
            }

        }

        [HttpPut]
        [Route("Update/{userId}")]
        public async Task<BaseResponseModel<bool>> UpdateUser(int userId, UserDto user)
        {
            if (!ModelState.IsValid)
                return new BaseResponseModel<bool>(StatusCodes.Status400BadRequest, "Model is not valid");

            var existingUser = await _userService.GetUserById(userId);
            if (existingUser == null)
                return new BaseResponseModel<bool>(StatusCodes.Status404NotFound, "User not found");

            var res = await _userService.UpdateUser(userId, user);
            if (res)
            {
                return new BaseResponseModel<bool>(StatusCodes.Status200OK, "Success", true);
            }
            else
            {
                return new BaseResponseModel<bool>(StatusCodes.Status500InternalServerError, "Error updating user", false);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<BaseResponseModel<bool>> DeleteUser(int userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user is null)
                return new BaseResponseModel<bool>(StatusCodes.Status404NotFound, "User Not Found or already deleted.");

            var result = await _userService.DeleteUser(userId);

            if (result)
            {
                return new BaseResponseModel<bool>(StatusCodes.Status200OK, "Success", true);
            }
            else
            {
                return new BaseResponseModel<bool>(StatusCodes.Status500InternalServerError, "Error deleting user", false);
            }
        }
    }
}
