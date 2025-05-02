using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface IVendaService
    {

        Task<List<Venda>> ObterTodosAsync();
        Task<Venda?> ObterPorIdAsync(int id);
        Task CriarAsync(Venda venda);
        Task<ResultadoOperacao> AtualizarAsync(Venda venda);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

