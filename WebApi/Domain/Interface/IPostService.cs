using Microsoft.Extensions.Hosting;
using WebApi.Dtos.PostDtos;

namespace WebApi.Domain.Interface
{
    public interface IPostService
    {
        Task<IEnumerable<PostDetailDto>> GetPosts();
        Task<string> CreatePost(PostDto post);
        Task<string> UpdatePost(int postId, PostDto post);
        Task<string> DeletePost(int postId);

        Task<IEnumerable<CommentDetailDto>> GetComments();
        Task<string> CreateComment(CommentDto comment);
        Task<string> UpdateComment(int commentId, CommentDto comment);
        Task<string> DeleteComment(int commentId);
    }
}
