using FaiscaSync.DTO;
using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface

{
    public interface IFuncionarioService
    {
        Task<List<Funcionario>> ObterTodosAsync();
        Task<Funcionario?> ObterPorIdAsync(int id);
        Task CriarAsync(FuncionarioDTO funcionarioDto);
        Task<ResultadoOperacao> AtualizarAsync(int id, FuncionarioDTO funcionarioDto);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}
