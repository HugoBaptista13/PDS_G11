using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class AquisicaoDTO
    {
        [Required]
        [StringLength(50)]
        public string Fornecedor { get; set; } = null!;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Valorpago { get; set; }

        [Required]
        public DateTime Dataaquisicao { get; set; }

        [Required]
        [StringLength(20)]
        public string Origemveiculo { get; set; } = null!;
    }
}
