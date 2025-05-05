using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class CargoDTO
    {
        [Required]
        [StringLength(50)]
        public string Nomecargo { get; set; } = null!;
    }
}
