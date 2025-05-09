using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class FaturaDTO
    {
        [Required]
        public DateTime DataEmissao { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal ValorFatura { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoPagamento { get; set; } = null!;

        [Required]
        public int IdCliente { get; set; }

        public int IdVendas { get; set; }

        public int IdManutencao { get; set; }
    }
}
