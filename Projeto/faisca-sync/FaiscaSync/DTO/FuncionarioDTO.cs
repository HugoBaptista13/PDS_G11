using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class FuncionarioDTO
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = null!;

        [Required]
        public DateTime DataContrato { get; set; }

        [Required]
        public DateTime DataNasc { get; set; }

        [Required]
        public int IdCargo { get; set; }

    }
}
