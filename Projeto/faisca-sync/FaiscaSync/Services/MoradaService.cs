using FaiscaSync.DTO;
using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class MoradaService : IMoradaService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public MoradaService(FsContext context, IConfiguration configuration )
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Morada>> ObterTodosAsync()
        {
            return await _context.Morada.ToListAsync();
        }

        public async Task<Morada?> ObterPorIdAsync(int id)
        {
            return await _context.Morada.FindAsync(id);
        }

        public async Task CriarAsync(MoradaDTO moradaDto)
        {
            var morada = new Morada
            {
                Rua = moradaDto.Rua,
                Numero = moradaDto.Numero,
                Descricaomorada = moradaDto.Descricaomorada,
                Pais = moradaDto.Pais,
                IdCpostal = moradaDto.IdCpostal
            };
            _context.Morada.Add(morada);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(int id, MoradaDTO moradaDto)
        {
            var moradaExistente = await _context.Morada.FindAsync(id);

            if (moradaExistente == null)
                return ResultadoOperacao.Falha("Falha na atualização da Morada! Morada não encontrada.");

            moradaExistente.Rua = moradaDto.Rua;
            moradaExistente.Numero = moradaDto.Numero;
            moradaExistente.Descricaomorada = moradaDto.Descricaomorada;
            moradaExistente.Pais = moradaDto.Pais;
            moradaExistente.IdCpostal = moradaDto.IdCpostal;

            _context.Entry(moradaExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Morada atualizada com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var morada = await _context.Morada.FindAsync(id);
            if (morada == null)
                return ResultadoOperacao.Falha("Falha na remoção da morada");
            _context.Morada.Remove(morada);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Morada removida com sucesso");
        }
    }
}
