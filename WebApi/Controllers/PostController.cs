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
        [Route("GetPosts")]
        public async Task<BaseResponseModel<PostDetailDto>> GetPosts()
        {
            var posts = await _postService.GetPosts();
            if (!posts.Any())
                return new BaseResponseModel<PostDetailDto>("500", "No post found.");

            return new BaseResponseModel<PostDetailDto>("200", "Success", posts.ToList());
        }

        [HttpPost]
        [Route("CreatePost")]
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

        [HttpPut]
        [Route("UpdatePost/{postId}")]
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

        [HttpDelete]
        [Route("DeletePost/{postId}")]
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



        [HttpGet]
        [Route("GetComments")]
        public async Task<BaseResponseModel<CommentDetailDto>> GetComments()
        {
            var comments = await _postService.GetComments();
            if (!comments.Any())
                return new BaseResponseModel<CommentDetailDto>("500", "No comment found");

            return new BaseResponseModel<CommentDetailDto>("200", "Success", comments.ToList());
        }

        [HttpPost]
        [Route("CreateComment")]
        public async Task<BaseResponseModel<bool>> CreateComment(CommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return new BaseResponseModel<bool>("400", "Model is not valid");

            try
            {
                var result = await _postService.CreateComment(commentDto);
                if (result == "Comment created successfully")
                    return new BaseResponseModel<bool>("200", "Success", true);
                else
                    return new BaseResponseModel<bool>("400", result, false);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>("500", ex.Message, false);
            }
        }

        [HttpPut]
        [Route("UpdateComment/{commentId}")]
        public async Task<BaseResponseModel<bool>> UpdateComment(int commentId, CommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return new BaseResponseModel<bool>("400", "Model is not valid");

            try
            {
                string result = await _postService.UpdateComment(commentId, commentDto);

                if (result == "Comment updated successfully")
                    return new BaseResponseModel<bool>("200", "Success", true);
                else
                    return new BaseResponseModel<bool>("400", result, false);

            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>("500", ex.Message, false);
            }
        }

        [HttpDelete]
        [Route("DeleteComment/{commentId}")]
        public async Task<BaseResponseModel<bool>> DeleteComment(int commentId)
        {
            try
            {
                bool isDeleted = await _postService.DeleteComment(commentId);

                if (!isDeleted)
                {
                    return new BaseResponseModel<bool>("500", "An error occurred while deleting the comment.", false);
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
