using FaiscaSync.DTO;
using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class EstadoVeiculoService : IEstadoVeiculoService
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

        public async Task CriarAsync(EstadoVeiculoDTO estadoVeiculoDto)
        {
            var estadoVeiculo = new EstadoVeiculo
            {
                Descricaoestadoveiculo = estadoVeiculoDto.Estado
            };
            _context.EstadoVeiculos.Add(estadoVeiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(int id, EstadoVeiculoDTO estadoVeiculoDto)
        {
            var estadoVeiculoExistente = await _context.EstadoVeiculos.FindAsync(id);

            if (estadoVeiculoExistente == null)
                return ResultadoOperacao.Falha("Falha na atualização do estado Veiculo! Estado Veiculo não encontrado.");

            estadoVeiculoExistente.Descricaoestadoveiculo = estadoVeiculoDto.Estado;

            _context.Entry(estadoVeiculoExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Estado Veiculo atualizado com Sucesso!");
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
