using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface ILoginService
    {
        Task<string?> AutenticarAsync(string username, string password); // Para login
        Task<IEnumerable<Login>> GetAllLoginsAsync();                    // Buscar todos
        Task<Login?> GetLoginByIdAsync(int id);                           // Buscar por ID
        Task<ResultadoOperacao> UpdateLoginAsync(Login login);                        // Atualizar
        Task<ResultadoOperacao> RemoverAsync(int id);                             // Remover
        Task CreateLoginAsync(Login login);                              //Criar

    }
}

