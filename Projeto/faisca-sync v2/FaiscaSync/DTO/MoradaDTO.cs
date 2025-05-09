using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class MoradaDTO
    {
        [Required]
        [StringLength(100)]
        public string Rua { get; set; } = null!;

        [Required]
        [StringLength(10)]
        public string Numero { get; set; } = null!;

        [StringLength(50)]
        public string Andar { get; set; }

        [Required]
        public int IdCpostal { get; set; }
    }
}
