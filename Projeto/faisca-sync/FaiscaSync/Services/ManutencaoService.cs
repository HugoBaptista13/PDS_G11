using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class ManutencaoService : IManutencaoService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public ManutencaoService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Manutencao>> ObterTodosAsync()
        {
            return await _context.Manutencaos.ToListAsync();
        }

        public async Task<Manutencao?> ObterPorIdAsync(int id)
        {
            return await _context.Manutencaos.FindAsync(id);
        }

        public async Task CriarAsync(Manutencao manutencao)
        {
            _context.Manutencaos.Add(manutencao);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync( Manutencao manutencao)
        {
            var exists = await _context.Manutencaos.AnyAsync(a => a.IdManutencao == manutencao.IdManutencao);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização da Manutenção!");

            _context.Entry(manutencao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Manutenção atualizada com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var manutencao = await _context.Manutencaos.FindAsync(id);
            if (manutencao == null)
                return ResultadoOperacao.Falha("Falha na remoção da manutenção");
            _context.Manutencaos.Remove(manutencao);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Manutenção removida com sucesso");
        }
    }
}
