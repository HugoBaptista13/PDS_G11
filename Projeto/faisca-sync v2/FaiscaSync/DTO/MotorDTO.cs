using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class MotorDTO
    {
        [Required]
        [StringLength(20)]
        public string TipoMotor { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Potencia { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Combustivel { get; set; } = null!;
    }
}