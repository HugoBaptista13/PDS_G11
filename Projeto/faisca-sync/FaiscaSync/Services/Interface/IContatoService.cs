using FaiscaSync.DTO;
using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface

{
    public interface IContatoService
    {
        Task<List<Contato>> ObterTodosAsync();
        Task<Contato?> ObterPorIdAsync(int id);
        Task CriarAsync(ContatoDTO contatoDto);
        Task<ResultadoOperacao> AtualizarAsync(int id, ContatoDTO contatoDto);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}
