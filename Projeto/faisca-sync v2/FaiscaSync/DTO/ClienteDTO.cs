using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class ClienteDTO
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = null!;

        [Required]
        public DateTime Datanasc { get; set; }

        [Required]
        [StringLength(9)]
        public string Nif { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Contato { get; set; } = null!;

        [Required]
        public int IdMorada { get; set; }
    }
}
