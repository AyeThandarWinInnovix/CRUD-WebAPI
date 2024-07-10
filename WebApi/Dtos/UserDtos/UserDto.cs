using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos.UserDtos
{
    public class UserDto
    {
        public int UserId { get; set; }

        [StringLength(255)]
        public string Username { get; set; } = null!;

        [StringLength(255)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
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
