using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FaiscaSync.Services
{
    public class ManutencaoService : IManutencaoService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ManutencaoService> _logger;

        public ManutencaoService(FsContext context, IConfiguration configuration, ILogger<ManutencaoService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<Manutencao>> ObterTodosAsync()
        {
            _logger.LogInformation("[ManutencaoService] Listagem de todas as manutenções.");
            return await _context.Manutencaos.ToListAsync();
        }

        public async Task<Manutencao?> ObterPorIdAsync(int id)
        {
            _logger.LogInformation($"[ManutencaoService] Consulta de manutenção ID {id}.");
            return await _context.Manutencaos.FindAsync(id);
        }

        public async Task CriarAsync(Manutencao manutencao)
        {
            _logger.LogInformation("[ManutencaoService] Criando nova manutenção.");
            _context.Manutencaos.Add(manutencao);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"[ManutencaoService] Manutenção criada com sucesso (ID: {manutencao.IdManutencao}).");
        }

        public async Task<ResultadoOperacao> AtualizarAsync(Manutencao manutencao)
        {
            _logger.LogInformation($"[ManutencaoService] Tentativa de atualização da manutenção ID {manutencao.IdManutencao}.");
            var exists = await _context.Manutencaos.AnyAsync(a => a.IdManutencao == manutencao.IdManutencao);
            if (!exists)
            {
                _logger.LogWarning($"[ManutencaoService] Falha: Manutenção ID {manutencao.IdManutencao} não encontrada para atualização.");
                return ResultadoOperacao.Falha("Falha na atualização da Manutenção!");
            }

            _context.Entry(manutencao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation($"[ManutencaoService] Manutenção ID {manutencao.IdManutencao} atualizada com sucesso.");
            return ResultadoOperacao.Ok("Manutenção atualizada com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            _logger.LogInformation($"[ManutencaoService] Tentando remover manutenção ID {id}.");
            var manutencao = await _context.Manutencaos.FindAsync(id);
            if (manutencao == null)
            {
                _logger.LogWarning($"[ManutencaoService] Falha: Manutenção ID {id} não encontrada para remoção.");
                return ResultadoOperacao.Falha("Falha na remoção da manutenção");
            }
            _context.Manutencaos.Remove(manutencao);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"[ManutencaoService] Manutenção ID {id} removida com sucesso.");
            return ResultadoOperacao.Ok("Manutenção removida com sucesso");
        }

        public async Task<ResultadoOperacao> IniciarDiagnosticoAsync(int manutencaoId, string descricao)
        {
            _logger.LogInformation($"[ManutencaoService] Iniciando diagnóstico para Manutenção ID {manutencaoId}.");
            var manutencao = await _context.Manutencaos.FindAsync(manutencaoId);
            if (manutencao == null)
            {
                _logger.LogWarning($"[ManutencaoService] Falha: Manutenção ID {manutencaoId} não encontrada.");
                return ResultadoOperacao.Falha("Manutenção não encontrada.");
            }

            if (manutencao.DataAgendada.HasValue && DateTime.Now < manutencao.DataAgendada.Value)
            {
                _logger.LogWarning($"[ManutencaoService] Diagnóstico não pode ser iniciado antes de {manutencao.DataAgendada.Value}.");
                return ResultadoOperacao.Falha($"Diagnóstico só pode ser iniciado a partir de {manutencao.DataAgendada.Value}.");
            }

            manutencao.Descricaomanutencao = descricao;
            manutencao.Datamanutencao = DateTime.Now;
            manutencao.IdEstadoManutencao = 1;

            await _context.SaveChangesAsync();
            _logger.LogInformation($"[ManutencaoService] Diagnóstico iniciado com sucesso para Manutenção ID {manutencaoId}.");
            return ResultadoOperacao.Ok("Diagnóstico iniciado com sucesso.");
        }

        public async Task<ResultadoOperacao> RealizarReparacaoAsync(int manutencaoId)
        {
            _logger.LogInformation($"[ManutencaoService] Iniciando reparação para Manutenção ID {manutencaoId}.");
            var m = await _context.Manutencaos.FindAsync(manutencaoId);
            if (m == null)
            {
                _logger.LogWarning($"[ManutencaoService] Falha: Manutenção ID {manutencaoId} não encontrada.");
                return ResultadoOperacao.Falha("Manutenção não encontrada.");
            }

            m.IdEstadoManutencao = 4;
            await _context.SaveChangesAsync();
            _logger.LogInformation($"[ManutencaoService] Reparação concluída com sucesso para Manutenção ID {manutencaoId}.");
            return ResultadoOperacao.Ok("Reparação realizada com sucesso.");
        }

        public async Task<ResultadoOperacao> AgendarManutencaoAsync(int manutencaoId, DateTime dataAgendada)
        {
            _logger.LogInformation($"[ManutencaoService] Agendando manutenção ID {manutencaoId} para {dataAgendada}.");
            var manutencao = await _context.Manutencaos.FindAsync(manutencaoId);
            if (manutencao == null)
            {
                _logger.LogWarning($"[ManutencaoService] Falha: Manutenção ID {manutencaoId} não encontrada.");
                return ResultadoOperacao.Falha("Manutenção não encontrada.");
            }

            manutencao.DataAgendada = dataAgendada;

            await _context.SaveChangesAsync();
            _logger.LogInformation($"[ManutencaoService] Manutenção ID {manutencaoId} agendada para {dataAgendada}.");
            return ResultadoOperacao.Ok($"Manutenção agendada para {dataAgendada}.");
        }
    }
}
