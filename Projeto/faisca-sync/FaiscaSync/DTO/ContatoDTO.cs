using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class ContatoDTO
    {
        [Required]
        [StringLength(100)]
        public string Contato { get; set; } = null!;

        [Required]
        public int IdCliente { get; set; }

        [Required]
        public int IdFuncionario { get; set; }

        [Required]
        public int IdTipoContato { get; set; }
    }
}
