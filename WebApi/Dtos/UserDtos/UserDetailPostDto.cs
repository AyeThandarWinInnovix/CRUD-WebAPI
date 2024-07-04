namespace WebApi.Dtos.UserDtos
{
    public class UserDetailPostDto
    {
        public int PostId { get; set; }

        public string Title { get; set; } = null!;

        public ICollection<UserDetailCommentDto> Comments { get; set; }
    }
}
