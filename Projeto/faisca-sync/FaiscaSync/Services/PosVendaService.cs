using FaiscaSync.DTO;
using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FaiscaSync.Services
{
    public class PosVendaService : IPosVendaService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PosVendaService> _logger;

        public PosVendaService(FsContext context, IConfiguration configuration, ILogger<PosVendaService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<PosVenda>> ObterTodosAsync()
        {
            _logger.LogInformation("[PosVenda] Listando todas as solicitações de pós-venda.");
            return await _context.PosVenda.ToListAsync();
        }

        public async Task<PosVenda?> ObterPorIdAsync(int id)
        {
            _logger.LogInformation($"[PosVenda] Buscando solicitação de pós-venda com ID {id}.");
            return await _context.PosVenda.FindAsync(id);
        }

        public async Task CriarAsync(PosVenda posVenda)
        {
            _logger.LogInformation($"[PosVenda] Criando nova solicitação de pós-venda (Venda ID: {posVenda.IdVendas}).");
            _context.PosVenda.Add(posVenda);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"[PosVenda] Solicitação de pós-venda criada com sucesso (ID: {posVenda.IdPosVenda}).");
        }

        public async Task<ResultadoOperacao> AtualizarAsync(PosVenda posVenda)
        {
            _logger.LogInformation($"[PosVenda] Tentando atualizar solicitação ID {posVenda.IdPosVenda}.");

            var exists = await _context.PosVenda.AnyAsync(a => a.IdPosVenda == posVenda.IdPosVenda);
            if (!exists)
            {
                _logger.LogWarning($"[PosVenda] Falha: Solicitação ID {posVenda.IdPosVenda} não encontrada para atualização.");
                return ResultadoOperacao.Falha("Falha na atualização da Pós-Venda!");
            }

            _context.Entry(posVenda).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation($"[PosVenda] Solicitação ID {posVenda.IdPosVenda} atualizada com sucesso.");
            return ResultadoOperacao.Ok("Pós-Venda atualizada com sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            _logger.LogInformation($"[PosVenda] Tentando remover solicitação de pós-venda ID {id}.");

            var posVenda = await _context.PosVenda.FindAsync(id);
            if (posVenda == null)
            {
                _logger.LogWarning($"[PosVenda] Falha: Solicitação ID {id} não encontrada para remoção.");
                return ResultadoOperacao.Falha("Falha na remoção da Pós-Venda.");
            }

            _context.PosVenda.Remove(posVenda);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"[PosVenda] Solicitação ID {id} removida com sucesso.");
            return ResultadoOperacao.Ok("Pós-Venda removida com sucesso.");
        }

        public async Task<ResultadoOperacao> CriarSolicitacaoAsync(CriarPosVendaDTO dto)
        {
            _logger.LogInformation($"[PosVenda] Funcionário está a criar uma solicitação de pós-venda para Venda ID {dto.IdVendas}.");

            var venda = await _context.Vendas.FindAsync(dto.IdVendas);
            if (venda == null)
            {
                _logger.LogWarning($"[PosVenda] Falha: Venda ID {dto.IdVendas} não encontrada.");
                return ResultadoOperacao.Falha("Venda não encontrada.");
            }

            var novaSolicitacao = new PosVenda
            {
                IdVendas = dto.IdVendas,
                IdGarantia = dto.IdGarantia
            };

            _context.PosVenda.Add(novaSolicitacao);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"[PosVenda] Solicitação de pós-venda criada com sucesso para Venda ID {dto.IdVendas} (Nova ID: {novaSolicitacao.IdPosVenda}).");
            return ResultadoOperacao.Ok("Solicitação criada com sucesso.");
        }
    }
}
