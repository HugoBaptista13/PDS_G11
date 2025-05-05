using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class MarcaDTO
    {
        [Required]
        [StringLength(50)]
        public string Marca { get; set; } = null!;
    }
}