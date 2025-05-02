using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FaiscaSync.Services
{
    public class LoginService : ILoginService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public LoginService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string?> AutenticarAsync(string username, string password)
        {
            var login = await _context.Logins
                .Include(l => l.IdFuncionarioNavigation)
                .FirstOrDefaultAsync(l => l.Username == username && l.Password == password);

            if (login == null)
            {
                Console.Error.WriteLine("Login Inválido");
                return null;
            }

            var funcionario = login.IdFuncionarioNavigation;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, funcionario.Nome),
                new Claim("IdFuncionario", funcionario.IdFuncionario.ToString()),
                new Claim("Username", login.Username),
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

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IEnumerable<Login>> GetAllLoginsAsync()
        {
            return await _context.Logins.ToListAsync();
        }

        public async Task<Login?> GetLoginByIdAsync(int id)
        {
            return await _context.Logins.FindAsync(id);
        }

        public async Task<ResultadoOperacao> UpdateLoginAsync(Login login)
        {
            var exists = await _context.Logins.AnyAsync(l => l.IdLogin == login.IdLogin);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização do Login!");

            _context.Entry(login).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Login atualizado com Sucesso!");
        }
        public async Task CreateLoginAsync(Login login)
        {
            _context.Logins.Add(login);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var login = await _context.Logins.FindAsync(id);
            if (login == null)
                return ResultadoOperacao.Falha("Falha na remoção do Login");

            _context.Logins.Remove(login);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Login removido com sucesso");
        }
    }
}
