using Microsoft.Extensions.Hosting;
using WebApi.Dtos;

namespace WebApi.Domain.Interface
{
    public interface IPostService
    {
        Task<IEnumerable<PostDetailDto>> GetPosts();
        Task<string> CreatePost(PostDto post);
        Task<string> UpdatePost(int postId, PostDto post);
        Task<bool> DeletePost(int postId);

        Task<IEnumerable<CommentDetailDto>> GetComments();
        Task<string> CreateComment(CommentDto comment);
        Task<string> UpdateComment(int commentId, CommentDto comment);
        Task<bool> DeleteComment(int commentId);
    }
}
