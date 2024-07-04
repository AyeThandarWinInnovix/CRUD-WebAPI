using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WebApi.Data;
using WebApi.Domain.Interface;
using WebApi.Dtos;

namespace WebApi.Domain.Service
{
    public class PostService : IPostService
    {
        private readonly MainDbContext _context;
        private readonly IDataAccess _dataAccess;

        public PostService(MainDbContext context, IDataAccess dataAccess)
        {
            _context = context;
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<PostDetailDto>> GetPosts()
        {
            var posts = await _context.TblPosts
                             .Include(p => p.User)
                             .Include(p => p.TblComments)
                             .Where(p => !p.IsDeleted && p.User.IsActive)
                             .ToListAsync();

            var postDetailDtos = posts.Select(p => new PostDetailDto
            {
                PostId = p.PostId,
                UserId = p.UserId,
                Title = p.Title,
                Content = p.Content,
                IsDeleted = p.IsDeleted,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                ImageUrl = p.ImageUrl,
                User = new UserDto
                {
                    Username = p.User.Username,
                    Email = p.User.Email
                },
                Comments = p.TblComments.Select(c => new CommentDto
                {
                    CommentId = c.CommentId,
                    Content = c.Content
                }).ToList()
            });

            return postDetailDtos;
        }

        public async Task<string> CreatePost(PostDto postDto)
        {
            var user = await _context.TblUsers.FindAsync(postDto.UserId);
            if (user == null)
                return "User not found";

            if (!user.IsActive)
                return "User is not active";

            try
            {
                var post = new TblPost
                {
                    UserId = postDto.UserId,
                    Title = postDto.Title,
                    Content = postDto.Content,
                    IsDeleted = postDto.IsDeleted,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    ImageUrl = postDto.ImageUrl
                };

                _context.Add(post);
                await _context.SaveChangesAsync();

                return "Post created successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "An error occurred while creating the post";
            }
        }

        public async Task<string> UpdatePost(int postId, PostDto postDto)
        {
            try
            {
                var existingPost = await _context.TblPosts.FindAsync(postId);
                if (existingPost == null)
                    return "Post not found";

                if (existingPost.UserId != postDto.UserId)
                    return "User is not authorized to update this post";

                var user = await _context.TblUsers.FindAsync(postDto.UserId);
                if (user == null)
                    return "User not found";

                if (!user.IsActive)
                    return "User is not active";

                existingPost.Title = postDto.Title;
                existingPost.Content = postDto.Content;
                existingPost.IsDeleted = postDto.IsDeleted;
                existingPost.UpdatedAt = DateTime.UtcNow;
                existingPost.ImageUrl = postDto.ImageUrl;

                await _context.SaveChangesAsync();

                return "Post updated successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "An error occurred while updating the post";
            }
        }

        public async Task<bool> DeletePost(int postId)
        {
            var existingPost = await _context.TblPosts
                            .Include(p => p.TblComments)
                            .Where(p => p.PostId == postId && !p.IsDeleted && p.User.IsActive)
                            .FirstOrDefaultAsync();

            try
            {
                if (existingPost != null)
                {
                    existingPost.IsDeleted = true;

                    foreach (var comment in existingPost.TblComments.ToList())
                    {
                        _context.TblComments.Remove(comment);
                    }

                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }



        public async Task<IEnumerable<CommentDetailDto>> GetComments()
        {
            var comments = await _context.TblComments
                             .Include(p => p.User)
                             .Include(p => p.Post)
                             .Where(p => p.User.IsActive)
                             .ToListAsync();

            var commentDetailDtos = comments.Select(p => new CommentDetailDto
            {
                CommentId = p.CommentId,
                PostId = p.PostId,
                UserId = p.UserId,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                User = new UserDto
                {
                    Username = p.User.Username,
                    Email = p.User.Email
                },
                Post = new PostDto
                {
                    Title = p.Post.Title,
                    Content = p.Post.Content
                }
            });

            return commentDetailDtos;
        }

        public async Task<string> CreateComment(CommentDto commentDto)
        {
            var user = await _context.TblUsers.FindAsync(commentDto.UserId);
            if (user == null)
                return "User not found";

            if (!user.IsActive)
                return "User is not active";

            var post = await _context.TblPosts.FindAsync(commentDto.PostId);
            if (post == null)
                return "Post not found";

            if (post.IsDeleted)
                return "Post is deleted";

            try
            {
                var comment = new TblComment
                {
                    PostId = commentDto.PostId,
                    UserId = commentDto.UserId,
                    Content = commentDto.Content,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Add(comment);
                await _context.SaveChangesAsync();

                return "Comment created successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "An error occurred while creating the comment";
            }
        }

        public async Task<string> UpdateComment(int commentId, CommentDto commentDto)
        {
            try
            {
                var existingComment = await _context.TblComments.FindAsync(commentId);
                if (existingComment == null)
                    return "Comment not found";

                var post = await _context.TblPosts.FindAsync(commentDto.PostId);
                if (post == null)
                    return "Post not found";

                var user = await _context.TblUsers.FindAsync(commentDto.UserId);
                if (user == null)
                    return "User not found";

                if (!user.IsActive)
                    return "User is not active";

                if (post.IsDeleted)
                    return "Post is deleted";

                if (existingComment.UserId != commentDto.UserId)
                    return "User is not authorized to update this comment";

                existingComment.Content = commentDto.Content;
                existingComment.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return "Comment updated successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "An error occurred while updating the comment";
            }
        }

        public async Task<bool> DeleteComment(int commentId)
        {
            var existingComment = await _context.TblComments
                            .Where(p => p.CommentId == commentId)
                            .FirstOrDefaultAsync();

            try
            {
                if (existingComment != null)
                {
                    _context.TblComments.Remove(existingComment);
                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
