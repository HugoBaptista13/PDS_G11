using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class MotorService : IMotorService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public MotorService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Motor>> ObterTodosAsync()
        {
            return await _context.Motors.ToListAsync();
        }

        public async Task<Motor?> ObterPorIdAsync(int id)
        {
            return await _context.Motors.FindAsync(id);
        }

        public async Task CriarAsync(Motor motor)
        {
            _context.Motors.Add(motor);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync( Motor motor)
        {
            var exists = await _context.Motors.AnyAsync(a => a.IdMotor == motor.IdMotor);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização do Motor!");

            _context.Entry(motor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Motor atualizado com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var motor = await _context.Motors.FindAsync(id);
            if (motor == null)
                return ResultadoOperacao.Falha("Falha na remoção do Motor");
            _context.Motors.Remove(motor);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Motor removido com sucesso");
        }
    }
}
