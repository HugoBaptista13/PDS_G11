using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface

{
    public interface IFuncionarioService
    {
        Task<List<Funcionario>> ObterTodosAsync();
        Task<Funcionario?> ObterPorIdAsync(int id);
        Task CriarAsync(Funcionario funcionario);
        Task<ResultadoOperacao> AtualizarAsync(Funcionario funcionario);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}
