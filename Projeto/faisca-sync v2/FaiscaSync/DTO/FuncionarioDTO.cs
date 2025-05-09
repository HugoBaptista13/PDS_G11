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
        [StringLength(20)]
        public string Contato { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string Username { get; set; } = null!;

        [Required]
        [StringLength(120)]
        public string Password { get; set; } = null!;

        [Required]
        public int IdCargo { get; set; }

    }
}
