using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class VendasDTO
    {
        [Required]
        public DateTime DataVenda { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal ValorVenda { get; set; }

        [Required]
        public int IdVeiculo { get; set; }

        [Required]
        public int IdCliente { get; set; }

        [Required]
        public int IdFuncionario { get; set; }

        public bool CriarGarantia { get; set; }
    }
}
