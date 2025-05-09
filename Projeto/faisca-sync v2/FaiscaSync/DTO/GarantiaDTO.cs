using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class GarantiaDTO
    {
        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataFim { get; set; }

        [Required]
        public int IdVendas { get; set; }

        [Required]
        public int IdEstadoGarantia { get; set; }
    }
}
