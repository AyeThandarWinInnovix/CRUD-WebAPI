using WebApi.Dtos.UserDtos;

namespace WebApi.Dtos.PostDtos
{
    public class CommentDetailDto
    {
        public int CommentId { get; set; }

        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Content { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public CommentUserDto User { get; set; }

        public CommentPostDto Post { get; set; }
    }
}
