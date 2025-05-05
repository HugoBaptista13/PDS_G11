using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class TipoContatoDTO
    {
        [Required]
        [StringLength(50)]
        public string TipoContato { get; set; } = null!;
    }
}
