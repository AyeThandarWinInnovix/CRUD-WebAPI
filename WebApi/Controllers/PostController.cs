using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using WebApi.Domain.Interface;
using WebApi.Dtos;
using WebApi.ResponseModels;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<BaseResponseModel<PostDetailDto>> GetPosts()
        {
            var posts = await _postService.GetPosts();
            if (!posts.Any())
                return new BaseResponseModel<PostDetailDto>("500", "No post found.");

            return new BaseResponseModel<PostDetailDto>("200", "Success", posts.ToList());
        }

        [HttpPost]
        public async Task<BaseResponseModel<bool>> CreatePost(PostDto postDto)
        {
            if (!ModelState.IsValid)
                return new BaseResponseModel<bool>("400", "Model is not valid");

            try
            {
                postDto.IsDeleted = false;
                var result = await _postService.CreatePost(postDto);
                if(result == "Post created successfully")
                    return new BaseResponseModel<bool>("200", "Success", true);
                else
                    return new BaseResponseModel<bool>("400", result, false);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>("500", ex.Message, false);
            }
        }

        [HttpPut("{postId}")]
        public async Task<BaseResponseModel<bool>> UpdatePost(int postId, PostDto postDto)
        {
            if(!ModelState.IsValid)
                return new BaseResponseModel<bool>("400", "Model is not valid");

            try
            {
                string result = await _postService.UpdatePost(postId, postDto);

                if(result == "Post updated successfully")
                    return new BaseResponseModel<bool>("200", "Success", true);
                else
                    return new BaseResponseModel<bool>("400", result, false);

            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>("500", ex.Message, false);
            }
        }

        [HttpDelete("{postId}")]
        public async Task<BaseResponseModel<bool>> DeletePost(int postId)
        {
            try
            {
                bool isDeleted = await _postService.DeletePost(postId);

                if (!isDeleted)
                {
                    return new BaseResponseModel<bool>("500", "An error occurred while deleting the post.", false);
                }

                return new BaseResponseModel<bool>("200", "Success", true);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>("500", ex.Message, false);
            }
        }
    }
}
