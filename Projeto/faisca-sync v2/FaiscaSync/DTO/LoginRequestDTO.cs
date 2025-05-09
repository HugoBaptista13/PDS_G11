using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        [StringLength(30)]
        public string Username { get; set; } = null!;

        [Required]
        [StringLength(120)]
        public string Password { get; set; } = null!;
    }
}
