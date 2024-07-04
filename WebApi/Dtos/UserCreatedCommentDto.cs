namespace WebApi.Dtos
{
    public class UserCreatedCommentDto
    {
        public int CommentId { get; set; }
        public string Content { get; set; } = null!;
        public int PostId { get; set; }
    }
}
