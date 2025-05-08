using FaiscaSync.DTO;
using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;

namespace FaiscaSync.Services
{
    public class LoginService : ILoginService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginService> _logger;

        public LoginService(FsContext context, IConfiguration configuration, ILogger<LoginService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string?> AutenticarAsync(string username, string password)
        {
            _logger.LogInformation($"[Login] Tentando autenticar utilizador {username}.");

            var login = await _context.Logins
                .Include(l => l.IdFuncionarioNavigation).ThenInclude(f => f.IdCargoNavigation)
                .FirstOrDefaultAsync(l => l.Username == username && l.Password == password);

            if (login == null)
            {
                _logger.LogWarning($"[Login] Falha na autenticação para o utilizador {username}.");
                return null;
            }

            var funcionario = login.IdFuncionarioNavigation;
            var role = login.IdFuncionarioNavigation.IdCargoNavigation.Nomecargo;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, funcionario.Nome),
                new Claim("IdFuncionario", funcionario.IdFuncionario.ToString()),
                new Claim("Username", login.Username),
                new Claim(ClaimTypes.Role , role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            _logger.LogInformation($"[Login] Utilizador {username} autenticado com sucesso. Token emitido.");
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IEnumerable<Login>> GetAllLoginsAsync()
        {
            _logger.LogInformation("[Admin] Listando todos os logins existentes.");
            return await _context.Logins.ToListAsync();
        }

        public async Task<Login?> GetLoginByIdAsync(int id)
        {
            _logger.LogInformation($"[Admin] Buscando login com ID {id}.");
            return await _context.Logins.FindAsync(id);
        }

        public async Task<ResultadoOperacao> CriarLoginAsync(NovoLoginDTO novoLogin)
        {
            _logger.LogInformation($"[Admin] Tentando criar login para Funcionário ID {novoLogin.IdFuncionario} com username {novoLogin.username}.");

            var funcionario = await _context.Funcionarios
                .Include(f => f.IdCargoNavigation)
                .FirstOrDefaultAsync(f => f.IdFuncionario == novoLogin.IdFuncionario);

            if (funcionario == null)
            {
                _logger.LogWarning($"[Admin] Funcionário ID {novoLogin.IdFuncionario} não encontrado para criação de login.");
                return ResultadoOperacao.Falha("Funcionário não encontrado.");
            }

            var existe = await _context.Logins
                .AnyAsync(l => l.IdFuncionario == novoLogin.IdFuncionario || l.Username == novoLogin.username);

            if (existe)
            {
                _logger.LogWarning($"[Admin] Já existe login para funcionário {novoLogin.IdFuncionario} ou username {novoLogin.username} já está em uso.");
                return ResultadoOperacao.Falha("Já existe login para esse funcionário ou username já em uso.");
            }

            var novo = new Login
            {
                Username = novoLogin.username,
                Password = novoLogin.password,
                IdFuncionario = novoLogin.IdFuncionario
            };

            _context.Logins.Add(novo);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"[Admin] Login criado com sucesso para funcionário {novoLogin.IdFuncionario}.");
            return ResultadoOperacao.Ok("Login criado com sucesso.");
        }

        public async Task<ResultadoOperacao> UpdateLoginAsync(Login login)
        {
            _logger.LogInformation($"[Admin] Tentando atualizar login ID {login.IdLogin}.");

            var exists = await _context.Logins.AnyAsync(l => l.IdLogin == login.IdLogin);
            if (!exists)
            {
                _logger.LogWarning($"[Admin] Falha na atualização: Login ID {login.IdLogin} não encontrado.");
                return ResultadoOperacao.Falha("Falha na atualização do Login!");
            }

            _context.Entry(login).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            _logger.LogInformation($"[Admin] Login ID {login.IdLogin} atualizado com sucesso.");
            return ResultadoOperacao.Ok("Login atualizado com Sucesso!");
        }

        public async Task CreateLoginAsync(Login login)
        {
            _logger.LogInformation($"[Admin] Criando login diretamente (ID Funcionário: {login.IdFuncionario}).");
            _context.Logins.Add(login);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"[Admin] Login criado diretamente com sucesso (ID Funcionário: {login.IdFuncionario}).");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            _logger.LogInformation($"[Admin] Tentando remover login ID {id}.");

            var login = await _context.Logins.FindAsync(id);
            if (login == null)
            {
                _logger.LogWarning($"[Admin] Login ID {id} não encontrado para remoção.");
                return ResultadoOperacao.Falha("Falha na remoção do Login");
            }

            _context.Logins.Remove(login);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"[Admin] Login ID {id} removido com sucesso.");
            return ResultadoOperacao.Ok("Login removido com sucesso");
        }
    }
}
