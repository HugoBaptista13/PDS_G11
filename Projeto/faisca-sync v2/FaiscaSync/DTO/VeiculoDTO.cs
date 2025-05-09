using System.ComponentModel.DataAnnotations;

namespace FaiscaSync.DTO
{
    public class VeiculoDTO
    {
        [Required]
        [StringLength(10)]
        public string Matricula { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Chassi { get; set; } = null!;

        [Required]
        public int AnoFabrico { get; set; }

        [Required]
        [StringLength(30)]
        public string Cor { get; set; } = null!;

        [Required]
        public int Quilometragem { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PrecoVenda { get; set; }

        [Required]
        [StringLength(50)]
        public string Fornecedor { get; set; } = null!;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal ValorPago { get; set; }

        [Required]
        public DateTime DataAquisicao { get; set; }

        [Required]
        [StringLength(20)]
        public string OrigemVeiculo { get; set; } = null!;

        [StringLength(200)]
        public string? Foto { get; set; }

        [StringLength(300)]
        public string? Descricao { get; set; }


        //Dados motor
        [Required]
        [StringLength(20)]
        public string TipoMotor { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Potencia { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Combustivel { get; set; } = null!;

        //Dados Marca
        [Required]
        [StringLength(50)]
        public string DescricaoMarca { get; set; } = null!;

        //Dados Modelo
        [Required]
        [StringLength(50)]
        public string NomeModelo { get; set; } = null!;

        [Required]
        public int IdTipoVeiculo { get; set; }
    }
}