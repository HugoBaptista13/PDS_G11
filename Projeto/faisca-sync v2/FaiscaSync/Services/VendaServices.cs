using FaiscaSync.DTO;
using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class VendaServices
    {
        private readonly FsContext _context;
        private readonly VeiculoServices _veiculoService;

        // Injeção de dependência do VeiculoServices
        public VendaServices(FsContext context, VeiculoServices veiculoService)
        {
            _context = context;
            _veiculoService = veiculoService;
        }

        // Lista todas as Vendas
        public async Task<List<Venda>> GetAllAsync()
        {
            return await _context.Vendas.ToListAsync();
        }

        // Obtém uma Venda por ID
        public async Task<Venda?> GetByIdAsync(int id)
        {
            return await _context.Vendas.FindAsync(id);
        }

        // Cria uma nova Venda
        public async Task<Venda> CreateAsync(VendasDTO vendaDto)
        {
            var venda = new Venda
            {
                Datavenda = vendaDto.DataVenda,
                Valorvenda = vendaDto.ValorVenda,
                IdCliente = vendaDto.IdCliente,
                IdVeiculo = vendaDto.IdVeiculo,
                IdFuncionario = vendaDto.IdFuncionario
            };

            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();

            // 🔄 Depois da venda criada, atualiza o veículo para "Vendido"
            var veiculo = await _context.Veiculos
                .FirstOrDefaultAsync(v => v.IdVeiculo == venda.IdVeiculo);

            if (veiculo != null)
            {
                var estadoVendido = await _context.EstadoVeiculos
                    .FirstOrDefaultAsync(e => e.Descricaoestadoveiculo == "Vendido");

                if (estadoVendido == null)
                    throw new Exception("Estado 'Vendido' não encontrado.");

                veiculo.IdEstadoVeiculo = estadoVendido.IdEstadoVeiculo;
                await _context.SaveChangesAsync();
            }

            if (vendaDto.CriarGarantia)
            {
                var garantia = new Garantia
                {
                    IdVendas = venda.IdVendas,
                    Datainicio = DateTime.UtcNow,
                    Datafim = DateTime.UtcNow.AddYears(5),
                    IdEstadoGarantia = 1
                };

                _context.Garantia.Add(garantia);
                await _context.SaveChangesAsync();
            }

            return venda;
        }

        // Atualiza uma Venda existente
        public async Task<bool> UpdateAsync(int id, Venda venda)
        {
            var existing = await _context.Vendas.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(venda);
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove uma Venda
        public async Task<bool> DeleteAsync(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
                return false;

            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
