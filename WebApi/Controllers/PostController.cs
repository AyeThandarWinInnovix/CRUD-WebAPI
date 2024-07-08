using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using WebApi.Domain.Interface;
using WebApi.Dtos.PostDtos;
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

        // Posts
        [HttpGet]
        [Route("GetPosts")]
        public async Task<BaseResponseModel<PostDetailDto>> GetPosts()
        {
            var posts = await _postService.GetPosts();
            if (!posts.Any())
                return new BaseResponseModel<PostDetailDto>(StatusCodes.Status404NotFound, "No post found.");

            return new BaseResponseModel<PostDetailDto>(StatusCodes.Status200OK, "Success", posts.ToList());
        }

        [HttpPost]
        [Route("CreatePost")]
        public async Task<BaseResponseModel<bool>> CreatePost(PostDto postDto)
        {
            if (!ModelState.IsValid)
                return new BaseResponseModel<bool>(StatusCodes.Status400BadRequest, "Model is not valid");

            try
            {
                postDto.IsDeleted = false;
                var result = await _postService.CreatePost(postDto);
                if(result == "Post created successfully")
                    return new BaseResponseModel<bool>(StatusCodes.Status200OK, "Success", true);
                else
                    return new BaseResponseModel<bool>(StatusCodes.Status400BadRequest, result, false);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>(StatusCodes.Status500InternalServerError, ex.Message, false);
            }
        }

        [HttpPut]
        [Route("UpdatePost/{postId}")]
        public async Task<BaseResponseModel<bool>> UpdatePost(int postId, PostDto postDto)
        {
            if(!ModelState.IsValid)
                return new BaseResponseModel<bool>(StatusCodes.Status400BadRequest, "Model is not valid");

            try
            {
                string result = await _postService.UpdatePost(postId, postDto);

                if(result == "Post updated successfully")
                    return new BaseResponseModel<bool>(StatusCodes.Status200OK, "Success", true);

                else if(result == "User does not have permission to update this post")
                    return new BaseResponseModel<bool>(StatusCodes.Status403Forbidden, result, false);

                else if(result == "Post not found" || result == "User not found" || result == "User is not active" || result == "Post is deleted")
                    return new BaseResponseModel<bool>(StatusCodes.Status404NotFound, "Not found", false);

                else
                    return new BaseResponseModel<bool>(StatusCodes.Status400BadRequest, result, false);

            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>(StatusCodes.Status500InternalServerError, ex.Message, false);
            }
        }

        [HttpDelete]
        [Route("DeletePost/{postId}")]
        public async Task<BaseResponseModel<bool>> DeletePost(int postId)
        {
            try
            {
                string result = await _postService.DeletePost(postId);

                if (result == "Post deleted successfully")
                    return new BaseResponseModel<bool>(StatusCodes.Status200OK, "Success", true);

                else if (result == "Post not found or already deleted")
                    return new BaseResponseModel<bool>(StatusCodes.Status404NotFound, result, false);

                else
                    return new BaseResponseModel<bool>(StatusCodes.Status500InternalServerError, result, false);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>(StatusCodes.Status500InternalServerError, ex.Message, false);
            }
        }


        // Comments
        [HttpGet]
        [Route("GetComments")]
        public async Task<BaseResponseModel<CommentDetailDto>> GetComments()
        {
            var comments = await _postService.GetComments();
            if (!comments.Any())
                return new BaseResponseModel<CommentDetailDto>(StatusCodes.Status404NotFound, "No comment found");

            return new BaseResponseModel<CommentDetailDto>(StatusCodes.Status200OK, "Success", comments.ToList());
        }

        [HttpPost]
        [Route("CreateComment")]
        public async Task<BaseResponseModel<bool>> CreateComment(CommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return new BaseResponseModel<bool>(StatusCodes.Status400BadRequest, "Model is not valid");

            try
            {
                var result = await _postService.CreateComment(commentDto);
                if (result == "Comment created successfully")
                    return new BaseResponseModel<bool>(StatusCodes.Status200OK, "Success", true);
                else
                    return new BaseResponseModel<bool>(StatusCodes.Status400BadRequest, result, false);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>(StatusCodes.Status500InternalServerError, ex.Message, false);
            }
        }

        [HttpPut]
        [Route("UpdateComment/{commentId}")]
        public async Task<BaseResponseModel<bool>> UpdateComment(int commentId, CommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return new BaseResponseModel<bool>(StatusCodes.Status400BadRequest, "Model is not valid");

            try
            {
                string result = await _postService.UpdateComment(commentId, commentDto);

                if (result == "Comment updated successfully")
                    return new BaseResponseModel<bool>(StatusCodes.Status200OK, "Success", true);

                else if (result == "User does not have permission to update this comment")
                    return new BaseResponseModel<bool>(StatusCodes.Status403Forbidden, result, false);

                else if (result == "Comment not found" || result == "Post not found" || result == "User not found" || result == "User is not active" || result == "Post is deleted")
                    return new BaseResponseModel<bool>(StatusCodes.Status404NotFound, "Not found", false);

                else
                    return new BaseResponseModel<bool>(StatusCodes.Status400BadRequest, result, false);

            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>(StatusCodes.Status500InternalServerError, ex.Message, false);
            }
        }

        [HttpDelete]
        [Route("DeleteComment/{commentId}")]
        public async Task<BaseResponseModel<bool>> DeleteComment(int commentId)
        {
            try
            {
                string result = await _postService.DeleteComment(commentId);

                if ( result == "Comment deleted successfully")
                    return new BaseResponseModel<bool>(StatusCodes.Status200OK, "Success", true);

                else if (result == "Comment not found or already deleted")
                    return new BaseResponseModel<bool>(StatusCodes.Status404NotFound, result, false);

                else
                    return new BaseResponseModel<bool>(StatusCodes.Status500InternalServerError, "An error occurred while deleting the comment.", false);

            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>(StatusCodes.Status500InternalServerError, ex.Message, false);
            }
        }

    }
}
