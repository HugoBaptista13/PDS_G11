using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public VeiculoService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Veiculo>> ObterTodosAsync()
        {
            return await _context.Veiculos.ToListAsync();
        }

        public async Task<Veiculo?> ObterPorIdAsync(int id)
        {
            return await _context.Veiculos.FindAsync(id);
        }

        public async Task CriarAsync(Veiculo veiculo)
        {
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync( Veiculo veiculo)
        {
            var exists = await _context.Veiculos.AnyAsync(a => a.IdVeiculo == veiculo.IdVeiculo);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização do Veiculo!");

            _context.Entry(veiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Veiculo atualizado com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var Veiculo = await _context.Veiculos.FindAsync(id);
            if (Veiculo == null)
                return ResultadoOperacao.Falha("Falha na remoção do Veiculo");
            _context.Veiculos.Remove(Veiculo);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Veiculo removido com sucesso");
        }
    }
}
