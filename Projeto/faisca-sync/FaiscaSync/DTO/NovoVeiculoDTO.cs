using FaiscaSync.Models;
using System;

namespace FaiscaSync.DTO
{
    public class NovoVeiculoDTO
    {
        public int IdModelo { get; set; }
        public int IdTipoVeiculo { get; set; }
        public int IdMotor { get; set; }
        public int IdEstadoVeiculo { get; set; }
        public string Matricula { get; set; } = string.Empty;
        public string Cor { get; set; } = string.Empty;
        public string Chassi { get; set; } = string.Empty;
        public int AnoFabrico { get; set; }
        public decimal Preco { get; set; }
        public decimal Quilometros { get; set; }
    }
}
