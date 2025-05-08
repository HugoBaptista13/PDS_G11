namespace FaiscaSync.DTO
{
    public class VeiculoRecenteDTO
    {
        public int IdVeiculo { get; set; }
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public int Ano { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataAquisicao { get; set; }
    }
}
