using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class UserDto
    {
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
    }
}
