namespace WebApi.Dtos
{
    public class UserDetailPostDto
    {
        public int PostId { get; set; }

        public string Title { get; set; } = null!;

        public ICollection<UserDetailCommentDto> Comments { get; set; }
    }
}
