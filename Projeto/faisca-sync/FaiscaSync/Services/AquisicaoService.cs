using FaiscaSync.DTO;
using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FaiscaSync.Services
{
    public class AquisicaoService : IAquisicaoService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AquisicaoService> _logger;

        public AquisicaoService(FsContext context, IConfiguration configuration, ILogger<AquisicaoService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<Aquisicao>> ObterTodosAsync()
        {
            _logger.LogInformation("Listagem de todas as aquisições.");
            return await _context.Aquisicaos.ToListAsync();
        }

        public async Task<Aquisicao?> ObterPorIdAsync(int id)
        {
            _logger.LogInformation($"Consulta da aquisição com ID {id}.");
            return await _context.Aquisicaos.FindAsync(id);
        }

        public async Task CriarAsync(Aquisicao aquisicao)
        {
            _context.Aquisicaos.Add(aquisicao);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Aquisição criada com ID {aquisicao.IdAquisicao}.");
        }

        public async Task<ResultadoOperacao> AtualizarAsync(Aquisicao aquisicao)
        {
            var exists = await _context.Aquisicaos.AnyAsync(a => a.IdAquisicao == aquisicao.IdAquisicao);
            if (!exists)
            {
                _logger.LogWarning($"Tentativa de atualizar aquisição {aquisicao.IdAquisicao} que não existe.");
                return ResultadoOperacao.Falha("Falha na atualização da Aquisição!");
            }

            _context.Entry(aquisicao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Aquisição {aquisicao.IdAquisicao} atualizada com sucesso.");
            return ResultadoOperacao.Ok("Aquisição atualizada com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var aquisicao = await _context.Aquisicaos.FindAsync(id);
            if (aquisicao == null)
            {
                _logger.LogWarning($"Tentativa de remover aquisição {id} que não existe.");
                return ResultadoOperacao.Falha("Falha na remoção da aquisição");
            }

            _context.Aquisicaos.Remove(aquisicao);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Aquisição {id} removida com sucesso.");
            return ResultadoOperacao.Ok("Aquisição removida com sucesso");
        }

        public async Task<ResultadoOperacao> SubmeterPedidoAsync(int funcionarioId)
        {
            var veiculosDisponiveis = await _context.Veiculos
                .CountAsync(v => v.IdEstadoVeiculoNavigation.Descricaoestadoveiculo == "Disponivel");

            if (veiculosDisponiveis >= 10)
            {
                _logger.LogWarning($"Funcionário {funcionarioId} tentou submeter pedido, mas há veículos suficientes ({veiculosDisponiveis} disponíveis).");
                return ResultadoOperacao.Falha("Ainda existem mais de 10 veículos disponíveis.");
            }

            var novaAquisicao = new Aquisicao
            {
                FuncionarioId = funcionarioId,
                Dataaquisicao = DateTime.Now,
                AprovadoFinanceiro = false,
                AprovadoAdmin = false,
                Finalizada = false
            };

            _context.Aquisicaos.Add(novaAquisicao);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Funcionário {funcionarioId} submeteu um novo pedido de aquisição com ID {novaAquisicao.IdAquisicao}.");
            return ResultadoOperacao.Ok("Pedido submetido com sucesso.");
        }

        public async Task<ResultadoOperacao> RejeitarFinanceiroAsync(int aquisicaoId)
        {
            var aquisicao = await _context.Aquisicaos.FindAsync(aquisicaoId);
            if (aquisicao == null)
            {
                _logger.LogWarning($"Financeiro tentou rejeitar aquisição {aquisicaoId}, mas não foi encontrada.");
                return ResultadoOperacao.Falha("Aquisição não encontrada.");
            }

            _context.Aquisicaos.Remove(aquisicao);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Financeiro rejeitou e removeu a aquisição {aquisicaoId}.");
            return ResultadoOperacao.Ok("Pedido de aquisição rejeitado e removido.");
        }

        public async Task<ResultadoOperacao> AprovarAdminAsync(int aquisicaoId, NovoVeiculoDTO dto)
        {
            var aquisicao = await _context.Aquisicaos.FindAsync(aquisicaoId);
            if (aquisicao == null)
            {
                _logger.LogWarning($"Admin tentou aprovar aquisição {aquisicaoId}, mas não foi encontrada.");
                return ResultadoOperacao.Falha("Aquisição não encontrada.");
            }

            if (!aquisicao.AprovadoFinanceiro)
            {
                _logger.LogWarning($"Admin tentou aprovar aquisição {aquisicaoId}, mas ainda não foi aprovada pelo Financeiro.");
                return ResultadoOperacao.Falha("A aquisição ainda não foi aprovada pelo Financeiro.");
            }

            aquisicao.AprovadoAdmin = true;
            aquisicao.Finalizada = true;

            var novoVeiculo = new Veiculo
            {
                IdAquisicao = aquisicaoId,
                IdModelo = dto.IdModelo,
                IdTipoVeiculo = dto.IdTipoVeiculo,
                IdMotor = dto.IdMotor,
                IdEstadoVeiculo = dto.IdEstadoVeiculo,
                Matricula = dto.Matricula,
                Cor = dto.Cor,
                Chassi = dto.Chassi,
                Anofabrico = dto.AnoFabrico,
                Preco = dto.Preco,
                Quilometros = dto.Quilometros
            };

            _context.Veiculos.Add(novoVeiculo);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Admin aprovou a aquisição {aquisicaoId} e criou o veículo {novoVeiculo.Matricula}.");
            return ResultadoOperacao.Ok("Aquisição aprovada e novo veículo inserido.");
        }
    }
}
