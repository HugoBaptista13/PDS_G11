using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class MarcaDTO
    {
        [Required]
        [StringLength(50)]
        public string DescricaoMarca { get; set; } = null!;
    }
}