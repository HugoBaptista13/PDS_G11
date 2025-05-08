using FaiscaSync.DTO;
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

        public async Task<IEnumerable<Veiculo>> PesquisaAvancadaAsync(PesquisaVeiculoDTO filtro)
        {
            var query = _context.Veiculos
                .Include(v => v.IdModeloNavigation)
                    .ThenInclude(m => m.IdMarcaNavigation)
                .Include(v => v.IdTipoVeiculoNavigation)
                .Include(v => v.IdMotorNavigation)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Marca))
                query = query.Where(v => v.IdModeloNavigation.IdMarcaNavigation.Descricaomarca.Contains(filtro.Marca));

            if (!string.IsNullOrWhiteSpace(filtro.Modelo))
                query = query.Where(v => v.IdModeloNavigation.Nomemodelo.Contains(filtro.Modelo));

            if (!string.IsNullOrWhiteSpace(filtro.TipoVeiculo))
                query = query.Where(v => v.IdTipoVeiculoNavigation.Descricaotveiculo.Contains(filtro.TipoVeiculo));

            if (!string.IsNullOrWhiteSpace(filtro.Combustivel))
                query = query.Where(v => v.IdMotorNavigation.Combustivel.Contains(filtro.Combustivel));

            if (filtro.AnoDe.HasValue)
                query = query.Where(v => v.Anofabrico >= filtro.AnoDe);

            if (filtro.AnoAte.HasValue)
                query = query.Where(v => v.Anofabrico <= filtro.AnoAte);

            if (filtro.PrecoDe.HasValue)
                query = query.Where(v => v.Preco >= filtro.PrecoDe);

            if (filtro.PrecoAte.HasValue)
                query = query.Where(v => v.Preco <= filtro.PrecoAte);

            if (filtro.QuilometrosDe.HasValue)
                query = query.Where(v => v.Quilometros >= filtro.QuilometrosDe);

            if (filtro.QuilometrosAte.HasValue)
                query = query.Where(v => v.Quilometros <= filtro.QuilometrosAte);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Veiculo>> ObterVeiculosEmDestaqueAsync()
        {
            return await _context.Veiculos
                .Include(v => v.Aquisicao)
                .Where(v => v.Aquisicao != null)
                .OrderByDescending(v => v.Aquisicao.Dataaquisicao)
                .Take(5)
                .ToListAsync();
        }

        public async Task<List<Veiculo>> ObterVeiculosDisponiveisAsync()
        {
            return await _context.Veiculos
                .Include(v => v.IdModeloNavigation)
                .Include(v => v.IdEstadoVeiculoNavigation)
                .Where(v => v.IdEstadoVeiculoNavigation.Descricaoestadoveiculo == "Disponível")
                .ToListAsync();
        }


    }
}
