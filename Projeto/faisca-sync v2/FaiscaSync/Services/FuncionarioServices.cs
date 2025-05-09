using FaiscaSync.DTO;
using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FaiscaSync.Services
{
    public class FuncionarioServices
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public FuncionarioServices(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Funcionario>> GetAllAsync()
        {
            return await _context.Funcionarios.ToListAsync();
        }

        public async Task<Funcionario?> GetByIdAsync(int id)
        {
            return await _context.Funcionarios.FindAsync(id);
        }

        public async Task<Funcionario> CreateAsync(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();
            return funcionario;
        }

        public async Task<bool> UpdateAsync(int id, Funcionario funcionario)
        {
            var existing = await _context.Funcionarios.FindAsync(id);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(funcionario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null) return false;

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string?> LoginAsync(LoginRequestDTO loginRequest)
        {
            // Busca o funcionario e inclui o Cargo (para saber o nome do cargo)
            var funcionario = await _context.Funcionarios
     .FirstOrDefaultAsync(f =>
         f.Username == loginRequest.Username &&
         f.Password == loginRequest.Password);

            if (funcionario == null)
            {
                Console.WriteLine($"[Login] Falha na autenticação para o utilizador {loginRequest.Username}.");
                return null;
            }

            // 2️⃣ Busca o Cargo separadamente
            var cargo = await _context.Cargos.FirstOrDefaultAsync(c => c.IdCargo == funcionario.IdCargo);

            if (cargo == null)
            {
                Console.WriteLine($"[Login] Cargo não encontrado para o utilizador {loginRequest.Username}.");
                return null;
            }

            // 3️⃣ Obtem o nome do cargo
            var role = cargo.Nomecargo;  // <-- CONFIRMA que o campo se chama 'Nomecargo'

            // 4️⃣ Claims
            var claims = new[]
            {
    new Claim(ClaimTypes.Name, funcionario.Nome),
    new Claim("IdFuncionario", funcionario.IdFuncionario.ToString()),
    new Claim("Username", funcionario.Username),
    new Claim(ClaimTypes.Role, role)
};

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
