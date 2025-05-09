using FaiscaSync.Models;
using FaiscaSync.DTO;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class VeiculoServices
    {
        private readonly FsContext _context;

        public VeiculoServices(FsContext context)
        {
            _context = context;
        }

        public async Task<List<Veiculo>> GetVeiculos()
        {
            return await _context.Veiculos.ToListAsync();
        }

        public async Task<Veiculo> GetVeiculo(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                throw new KeyNotFoundException($"Veiculo with ID {id} not found.");
            }
            return veiculo;
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
                query = query.Where(v => v.Precovenda >= filtro.PrecoDe);

            if (filtro.PrecoAte.HasValue)
                query = query.Where(v => v.Precovenda <= filtro.PrecoAte);

            if (filtro.QuilometrosDe.HasValue)
                query = query.Where(v => v.Quilometros >= filtro.QuilometrosDe);

            if (filtro.QuilometrosAte.HasValue)
                query = query.Where(v => v.Quilometros <= filtro.QuilometrosAte);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Veiculo>> GetVeiculosDestaque()
        {
            return await _context.Veiculos
                .Include(v => v.IdModeloNavigation)
                .OrderByDescending(v => v.Dataaquisicao)
                .Take(5)
                .ToListAsync();
        }

        public async Task<List<Veiculo>> GetVeiculosDisponiveis()
        {
            return await _context.Veiculos
                .Include(v => v.IdModeloNavigation)
                .Include(v => v.IdEstadoVeiculoNavigation)
                .Where(v => v.IdEstadoVeiculoNavigation.Descricaoestadoveiculo == "Disponível")
                .ToListAsync();
        }

        public async Task<Veiculo> CreateAsync(Veiculo veiculo)
        {
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();
            return veiculo;
        }

        public async Task<bool> UpdateAsync(int id, Veiculo veiculo)
        {
            var existing = await _context.Veiculos.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(veiculo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
                return false;

            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AtualizarEstadoParaVendidoAsync(int veiculoId)
        {
            var veiculo = await _context.Veiculos
                .FirstOrDefaultAsync(v => v.IdVeiculo == veiculoId);

            if (veiculo != null)
            {
                var estadoVendido = await _context.EstadoVeiculos
                    .FirstOrDefaultAsync(e => e.Descricaoestadoveiculo == "Vendido");

                if (estadoVendido != null)
                {
                    veiculo.IdEstadoVeiculo = estadoVendido.IdEstadoVeiculo;
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
