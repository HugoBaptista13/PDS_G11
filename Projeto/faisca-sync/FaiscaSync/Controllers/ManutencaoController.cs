using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using FaiscaSync.DTO;
using Microsoft.Extensions.Logging;

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManutencaoController : ControllerBase
    {
        private readonly IManutencaoService _manutencaoService;
        private readonly ILogger<ManutencaoController> _logger;

        public ManutencaoController(IManutencaoService manutencaoService, ILogger<ManutencaoController> logger)
        {
            _manutencaoService = manutencaoService;
            _logger = logger;
        }

        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostrar-todas-manutencoes")]
        public async Task<ActionResult<IEnumerable<Manutencao>>> GetManutencaos()
        {
            _logger.LogInformation("[ManutencaoController] Iniciada listagem de todas as manutenções.");
            var manutencao = await _manutencaoService.ObterTodosAsync();
            return Ok(manutencao);
        }

        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostrar-manutencao-{id}")]
        public async Task<ActionResult<Manutencao>> GetManutencao(int id)
        {
            _logger.LogInformation($"[ManutencaoController] Consulta de manutenção ID {id}.");
            var manutencao = await _manutencaoService.ObterPorIdAsync(id);

            if (manutencao == null)
            {
                _logger.LogWarning($"[ManutencaoController] Manutenção ID {id} não encontrada.");
                return NotFound();
            }

            return Ok(manutencao);
        }

        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpPut("atualizar-manutencao-{id}")]
        public async Task<IActionResult> PutManutencao(int id, [FromBody] Manutencao manutencao)
        {
            if (id != manutencao.IdManutencao)
            {
                _logger.LogWarning($"[ManutencaoController] Falha na atualização: ID no URL ({id}) não bate com o objeto ({manutencao.IdManutencao}).");
                return BadRequest("ID no URL e ID no objeto não coincidem.");
            }

            _logger.LogInformation($"[ManutencaoController] Atualização solicitada para manutenção ID {id}.");
            var updated = await _manutencaoService.AtualizarAsync(manutencao);

            if (!updated.Sucesso)
            {
                _logger.LogWarning($"[ManutencaoController] Falha na atualização da manutenção ID {id}: {updated.Mensagem}");
                return NotFound(new { mensagem = updated.Mensagem });
            }

            _logger.LogInformation($"[ManutencaoController] Manutenção ID {id} atualizada com sucesso.");
            return Ok(new { mensagem = updated.Mensagem });
        }

        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpPost("criar-manutencao")]
        public async Task<ActionResult<Manutencao>> PostManutencao([FromBody] Manutencao manutencao)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("[ManutencaoController] Falha ao criar manutenção: ModelState inválido.");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("[ManutencaoController] Criando nova manutenção.");
            await _manutencaoService.CriarAsync(manutencao);
            _logger.LogInformation($"[ManutencaoController] Manutenção criada com sucesso (ID: {manutencao.IdManutencao}).");

            return CreatedAtAction(nameof(GetManutencao), new { id = manutencao.IdManutencao }, manutencao);
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("apagar-manutencao-{id}")]
        public async Task<IActionResult> DeleteManutencao(int id)
        {
            _logger.LogInformation($"[ManutencaoController] Tentando apagar manutenção ID {id}.");
            var deleted = await _manutencaoService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                _logger.LogWarning($"[ManutencaoController] Falha ao apagar manutenção ID {id}: {deleted.Mensagem}");
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            _logger.LogInformation($"[ManutencaoController] Manutenção ID {id} apagada com sucesso.");
            return Ok(new { mensagem = deleted.Mensagem });
        }

        [Authorize(Roles = "Funcionário")]
        [HttpPost("iniciar-diagnostico/{manutencaoId}")]
        public async Task<IActionResult> IniciarDiagnostico(int manutencaoId, [FromBody] string descricao)
        {
            _logger.LogInformation($"[ManutencaoController] Funcionário iniciou diagnóstico para Manutenção ID {manutencaoId}.");
            var resultado = await _manutencaoService.IniciarDiagnosticoAsync(manutencaoId, descricao);
            if (!resultado.Sucesso)
            {
                _logger.LogWarning($"[ManutencaoController] Falha ao iniciar diagnóstico para Manutenção ID {manutencaoId}: {resultado.Mensagem}");
                return BadRequest(resultado.Mensagem);
            }

            _logger.LogInformation($"[ManutencaoController] Diagnóstico iniciado com sucesso para Manutenção ID {manutencaoId}.");
            return Ok(resultado.Mensagem);
        }

        [Authorize(Roles = "Funcionário")]
        [HttpPut("realizar-reparacao/{manutencaoId}")]
        public async Task<IActionResult> RealizarReparacao(int manutencaoId)
        {
            _logger.LogInformation($"[ManutencaoController] Funcionário iniciou reparação para Manutenção ID {manutencaoId}.");
            var resultado = await _manutencaoService.RealizarReparacaoAsync(manutencaoId);
            if (!resultado.Sucesso)
            {
                _logger.LogWarning($"[ManutencaoController] Falha ao realizar reparação para Manutenção ID {manutencaoId}: {resultado.Mensagem}");
                return BadRequest(new { mensagem = resultado.Mensagem });
            }

            _logger.LogInformation($"[ManutencaoController] Reparação concluída com sucesso para Manutenção ID {manutencaoId}.");
            return Ok(new { mensagem = resultado.Mensagem });
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("agendar/{manutencaoId}")]
        public async Task<IActionResult> AgendarManutencao(int manutencaoId, [FromBody] DateTime dataAgendada)
        {
            _logger.LogInformation($"[ManutencaoController] Agendamento solicitado para Manutenção ID {manutencaoId} na data {dataAgendada}.");
            var resultado = await _manutencaoService.AgendarManutencaoAsync(manutencaoId, dataAgendada);
            if (!resultado.Sucesso)
            {
                _logger.LogWarning($"[ManutencaoController] Falha no agendamento para Manutenção ID {manutencaoId}: {resultado.Mensagem}");
                return BadRequest(resultado.Mensagem);
            }

            _logger.LogInformation($"[ManutencaoController] Manutenção ID {manutencaoId} agendada com sucesso para {dataAgendada}.");
            return Ok(resultado.Mensagem);
        }
    }
}
