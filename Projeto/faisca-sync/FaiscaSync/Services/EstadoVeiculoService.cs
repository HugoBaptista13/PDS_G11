using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class EstadoVeiculoService : IEstadoVeiucloService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public EstadoVeiculoService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<EstadoVeiculo>> ObterTodosAsync()
        {
            return await _context.EstadoVeiculos.ToListAsync();
        }

        public async Task<EstadoVeiculo?> ObterPorIdAsync(int id)
        {
            return await _context.EstadoVeiculos.FindAsync(id);
        }

        public async Task CriarAsync(EstadoVeiculo estadoVeiculo)
        {
            _context.EstadoVeiculos.Add(estadoVeiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync( EstadoVeiculo estadoVeiculo)
        {
            var exists = await _context.EstadoVeiculos.AnyAsync(a => a.IdEstadoVeiculo == estadoVeiculo.IdEstadoVeiculo);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização do Estado Veiculo!");

            _context.Entry(estadoVeiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Estado Veiculo atualizada com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var estadoVeiculo  = await _context.EstadoVeiculos.FindAsync(id);
            if (estadoVeiculo == null)
                return ResultadoOperacao.Falha("Falha na remoção do estado Veiculo");
            _context.EstadoVeiculos.Remove(estadoVeiculo);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Estado Veiculo removido com sucesso");
        }
    }
}
