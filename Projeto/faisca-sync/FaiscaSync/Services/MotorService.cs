using FaiscaSync.DTO;
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

        public async Task CriarAsync(MotorDTO motorDto)
        {
            var motor = new Motor
            {
                Tipomotor = motorDto.TipoMotor,
                Potencia = motorDto.Potencia,
                Combustivel = motorDto.Combustivel
            };

            _context.Motors.Add(motor);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(int id, MotorDTO motorDto)
        {
            var motorExistente = await _context.Motors.FindAsync(id);

            if (motorExistente == null)
                return ResultadoOperacao.Falha("Falha na atualização do Motor! Motor não encontrado.");

            motorExistente.Tipomotor = motorDto.TipoMotor;
            motorExistente.Potencia = motorDto.Potencia;
            motorExistente.Combustivel = motorDto.Combustivel;

            _context.Entry(motorExistente).State = EntityState.Modified;
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
