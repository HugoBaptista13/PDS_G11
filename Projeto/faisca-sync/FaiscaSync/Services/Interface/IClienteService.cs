using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface IClienteService
    {
        Task<List<Cliente>> ObterTodosAsync();
        Task<Cliente?> ObterPorIdAsync(int id);
        Task CriarAsync(Cliente cliente);
        Task<ResultadoOperacao> AtualizarAsync(Cliente cliente);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}
