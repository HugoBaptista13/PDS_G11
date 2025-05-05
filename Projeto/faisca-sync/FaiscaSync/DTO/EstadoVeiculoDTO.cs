using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class EstadoVeiculoDTO
    {
        [Required]
        [StringLength(50)]
        public string Estado { get; set; } = null!;
    }
}