using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class ModeloDTO
    {
        [Required]
        [StringLength(50)]
        public string NomeModelo { get; set; } = null!;
        [Required]
        public int IdMarca { get; set; }
    }
}