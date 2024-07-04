using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos.UserDtos
{
    public class UserDetailDto
    {
        public int UserId { get; set; }

        [StringLength(255)]
        public string Username { get; set; } = null!;

        [StringLength(255)]
        public string Password { get; set; } = null!;

        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = null!;

        [StringLength(255)]
        public string Role { get; set; } = null!;

        public bool IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<UserDetailPostDto> Posts { get; set; }
        public ICollection<UserCreatedCommentDto> Comments { get; set; }
    }
}
