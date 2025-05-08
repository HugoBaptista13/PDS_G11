using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FaiscaSync.Services
{
    public class VendaService : IVendaService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<VendaService> _logger;

        public VendaService(FsContext context, IConfiguration configuration, ILogger<VendaService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<Venda>> ObterTodosAsync()
        {
            _logger.LogInformation("[Venda] Listagem de todas as vendas.");
            return await _context.Vendas.ToListAsync();
        }

        public async Task<Venda?> ObterPorIdAsync(int id)
        {
            _logger.LogInformation($"[Venda] Consultando venda com ID {id}.");
            return await _context.Vendas.FindAsync(id);
        }

        public async Task CriarAsync(Venda venda)
        {
            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"[Venda] Venda criada com ID {venda.IdVendas}.");
        }

        public async Task<ResultadoOperacao> AtualizarAsync(Venda venda)
        {
            var exists = await _context.Vendas.AnyAsync(a => a.IdVendas == venda.IdVendas);
            if (!exists)
            {
                _logger.LogWarning($"[Venda] Tentativa de atualizar venda {venda.IdVendas}, mas não existe.");
                return ResultadoOperacao.Falha("Falha na atualização da Venda!");
            }

            _context.Entry(venda).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation($"[Venda] Venda {venda.IdVendas} atualizada com sucesso.");
            return ResultadoOperacao.Ok("Venda atualizada com sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
            {
                _logger.LogWarning($"[Venda] Tentativa de remover venda {id}, mas não existe.");
                return ResultadoOperacao.Falha("Falha na remoção da venda");
            }

            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"[Venda] Venda {id} removida com sucesso.");
            return ResultadoOperacao.Ok("Venda removida com sucesso");
        }

        public async Task<ResultadoOperacao> ReservarVeiculoAsync(int veiculoId, int funcionarioId)
        {
            _logger.LogInformation($"[Reserva] Funcionário {funcionarioId} iniciou reserva para Veículo {veiculoId}.");

            var veiculo = await _context.Veiculos
                .Include(v => v.IdEstadoVeiculoNavigation)
                .FirstOrDefaultAsync(v => v.IdVeiculo == veiculoId);

            if (veiculo == null)
            {
                _logger.LogWarning($"[Reserva] Falha: Veículo {veiculoId} não encontrado.");
                return ResultadoOperacao.Falha("Veículo não encontrado.");
            }

            if (veiculo.IdEstadoVeiculoNavigation.Descricaoestadoveiculo != "Disponível")
            {
                _logger.LogWarning($"[Reserva] Falha: Veículo {veiculoId} não está disponível para reserva.");
                return ResultadoOperacao.Falha("Veículo não está disponível para reserva.");
            }

            veiculo.IdEstadoVeiculo = await _context.EstadoVeiculos
                .Where(e => e.Descricaoestadoveiculo == "Reservado")
                .Select(e => e.IdEstadoVeiculo)
                .FirstOrDefaultAsync();

            var novaVenda = new Venda
            {
                IdVeiculo = veiculoId,
                IdFuncionario = funcionarioId,
                DataProposta = DateTime.Now,
                AprovadoFinanceiro = false,
                Concluida = false
            };

            _context.Vendas.Add(novaVenda);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"[Reserva] Veículo {veiculoId} reservado e proposta de venda criada (Venda ID {novaVenda.IdVendas}).");
            return ResultadoOperacao.Ok("Veículo reservado e proposta de venda criada com sucesso.");
        }

        public async Task<ResultadoOperacao> SubmeterVendaAsync(int veiculoId, int funcionarioId)
        {
            _logger.LogInformation($"[Submissão] Funcionário {funcionarioId} está a submeter venda para Veículo {veiculoId}.");

            var veiculo = await _context.Veiculos
                .Include(v => v.IdEstadoVeiculoNavigation)
                .FirstOrDefaultAsync(v => v.IdVeiculo == veiculoId);

            if (veiculo == null || veiculo.IdEstadoVeiculoNavigation.Descricaoestadoveiculo != "Reservado")
            {
                _logger.LogWarning($"[Submissão] Falha: Veículo {veiculoId} não está reservado ou não encontrado.");
                return ResultadoOperacao.Falha("Veículo não está reservado.");
            }

            var venda = new Venda
            {
                IdVeiculo = veiculoId,
                IdFuncionario = funcionarioId,
                DataProposta = DateTime.Now,
                AprovadoFinanceiro = false,
                Concluida = false
            };

            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"[Submissão] Proposta de venda submetida com sucesso para Veículo {veiculoId} (Venda ID {venda.IdVendas}).");
            return ResultadoOperacao.Ok("Proposta de venda submetida.");
        }

        public async Task<ResultadoOperacao> AprovarFinanceiroAsync(int vendaId)
        {
            _logger.LogInformation($"[Financeiro] Iniciando aprovação para Venda ID {vendaId}.");

            var venda = await _context.Vendas
                .Include(v => v.IdVeiculoNavigation)
                .FirstOrDefaultAsync(v => v.IdVendas == vendaId);

            if (venda == null)
            {
                _logger.LogWarning($"[Financeiro] Falha: Venda {vendaId} não encontrada.");
                return ResultadoOperacao.Falha("Venda não encontrada.");
            }

            if (venda.AprovadoFinanceiro)
            {
                _logger.LogInformation($"[Financeiro] Venda {vendaId} já estava aprovada.");
                return ResultadoOperacao.Falha("Venda já aprovada.");
            }

            venda.AprovadoFinanceiro = true;

            var fatura = new Fatura
            {
                IdVendas = vendaId,
                Dataemissao = DateTime.Now,
                Valorfatura = venda.Valorvenda
            };
            _context.Faturas.Add(fatura);

            await _context.SaveChangesAsync();

            var pagamento = new Pagamento
            {
                IdFatura = fatura.IdFatura,
                IdTipoPagamento = 1,
                Datapagamento = DateTime.Now,
                Valorpagamento = fatura.Valorfatura
            };
            _context.Pagamentos.Add(pagamento);

            await _context.SaveChangesAsync();

            _logger.LogInformation($"[Financeiro] Venda {vendaId} aprovada. Fatura {fatura.IdFatura} emitida e pagamento registado.");
            return ResultadoOperacao.Ok("Venda aprovada, fatura emitida e pagamento registado.");
        }

        public async Task<ResultadoOperacao> FinalizarVendaAdminAsync(int vendaId, int funcionarioId)
        {
            _logger.LogInformation($"[Admin] Iniciando finalização da Venda {vendaId} pelo Funcionário {funcionarioId}.");

            var venda = await _context.Vendas
                .Include(v => v.IdVeiculoNavigation)
                .FirstOrDefaultAsync(v => v.IdVendas == vendaId);

            if (venda == null)
            {
                _logger.LogWarning($"[Admin] Falha: Venda {vendaId} não encontrada.");
                return ResultadoOperacao.Falha("Venda não encontrada.");
            }

            if (!venda.AprovadoFinanceiro)
            {
                _logger.LogWarning($"[Admin] Venda {vendaId} ainda não aprovada pelo financeiro.");
                return ResultadoOperacao.Falha("Venda ainda não foi aprovada pelo Financeiro.");
            }

            if (venda.Concluida)
            {
                _logger.LogInformation($"[Admin] Venda {vendaId} já estava concluída.");
                return ResultadoOperacao.Falha("Venda já foi concluída.");
            }

            var vendidoId = await _context.EstadoVeiculos
                .Where(e => e.Descricaoestadoveiculo == "Vendido")
                .Select(e => e.IdEstadoVeiculo)
                .FirstAsync();

            var veiculo = venda.IdVeiculoNavigation;
            veiculo.IdEstadoVeiculo = vendidoId;
            venda.Concluida = true;

            await _context.SaveChangesAsync();

            _logger.LogInformation($"[Admin] Venda {vendaId} finalizada. Veículo {veiculo.IdVeiculo} marcado como VENDIDO.");

            await RegistarHistoricoEstadoVeiculoAsync(veiculo.IdVeiculo, vendidoId, funcionarioId);

            return ResultadoOperacao.Ok("Venda finalizada e veículo marcado como vendido.");
        }

        private async Task RegistarHistoricoEstadoVeiculoAsync(int veiculoId, int novoEstadoId, int funcionarioId)
        {
            var historico = new HistoricoEstadoVeiculo
            {
                IdVeiculo = veiculoId,
                IdEstadoVeiculo = novoEstadoId,
                IdFuncionario = funcionarioId,
                Dataalteracao = DateTime.Now
            };

            _context.HistoricoEstadoVeiculos.Add(historico);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"[Histórico] Estado do Veículo {veiculoId} registado no histórico (Estado: {novoEstadoId}, Funcionário: {funcionarioId}).");
        }
    }
}
