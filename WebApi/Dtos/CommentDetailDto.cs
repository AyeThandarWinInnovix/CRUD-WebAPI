namespace WebApi.Dtos
{
    public class CommentDetailDto
    {
        public int CommentId { get; set; }

        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Content { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public UserDto User { get; set; }

        public PostDto Post { get; set; }
    }
}
