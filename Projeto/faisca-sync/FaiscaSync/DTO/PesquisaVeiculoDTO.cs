namespace FaiscaSync.DTO
{
    public class PesquisaVeiculoDTO
    {
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? TipoVeiculo { get; set; }
        public int? AnoDe { get; set; }
        public int? AnoAte { get; set; }
        public decimal? PrecoDe { get; set; }
        public decimal? PrecoAte { get; set; }
        public decimal? QuilometrosDe { get; set; }
        public decimal? QuilometrosAte { get; set; }
        public string? Combustivel { get; set; } // Supondo que seja tipo de motor, podes adaptar
    }
}
