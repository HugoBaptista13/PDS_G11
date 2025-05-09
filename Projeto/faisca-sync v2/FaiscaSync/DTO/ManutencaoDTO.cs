using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class ManutencaoDTO
    {
        [Required]
        public DateTime DataManutencao { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoManutencao { get; set; } = null!;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Custo { get; set; }

        [Required]
        [StringLength(100)]
        public string DescricaoManutencao { get; set; } = null!;

        [Required]
        public int IdVeiculo { get; set; }

        [Required]
        public int IdGarantia { get; set; }

        [Required]
        public int IdEstadoManutencao { get; set; }

        [Required]
        public int IdFuncionario { get; set; }
    }
}
