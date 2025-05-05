using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class CpostalDTO
    {
        [Required]
        [StringLength(50)]
        public string Localidade { get; set; } = null!;
    }
}
