using FaiscaSync.DTO;
using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class TipoVeiculoService : ITipoVeiculoService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;
        public TipoVeiculoService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<TipoVeiculo>> ObterTodosAsync()
        {
            return await _context.TipoVeiculos.ToListAsync();
        }

        public async Task<TipoVeiculo?> ObterPorIdAsync(int id)
        {
            return await _context.TipoVeiculos.FindAsync(id);
        }

        public async Task CriarAsync(TipoVeiculoDTO tipoVeiculoDto)
        {
            var tipoVeiculo = new TipoVeiculo
            {
                Descricaotveiculo = tipoVeiculoDto.Tipo
            };

            _context.TipoVeiculos.Add(tipoVeiculo);
            await _context.SaveChangesAsync();
        }

        public async  Task<ResultadoOperacao> AtualizarAsync(int id, TipoVeiculoDTO tipoVeiculoDto)
        {
            var tipoVeiculoExistente = await _context.TipoVeiculos.FindAsync(id);

            if (tipoVeiculoExistente == null)
                return ResultadoOperacao.Falha("Falha na atualização do Tipo de Veiculo! Tipo de Veiculo não encontrado.");

            tipoVeiculoExistente.Descricaotveiculo = tipoVeiculoDto.Tipo;

            _context.Entry(tipoVeiculoExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Tipo de Veiculo atualizado com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var tipoVeiculo = await _context.TipoVeiculos.FindAsync(id);
            if (tipoVeiculo == null)
                return ResultadoOperacao.Falha("Falha na remoção do tipo de Veiculo");
            _context.TipoVeiculos.Remove(tipoVeiculo);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Tipo de Veiculo removido com sucesso");
        }
    }
}
