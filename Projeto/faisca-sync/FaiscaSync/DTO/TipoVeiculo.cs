using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class TipoVeiculoDTO
    {
        [Required]
        [StringLength(30)]
        public string Tipo { get; set; } = null!;
    }
}