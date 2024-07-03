namespace WebApi.Dtos
{
    public class PostDto
    {
        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? ImagePath { get; set; }
    }
}
