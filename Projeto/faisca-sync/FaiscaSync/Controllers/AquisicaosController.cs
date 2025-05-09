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
    public class AquisicaosController : ControllerBase
    {
        private readonly IAquisicaoService _aquisicaoService;
        private readonly ILogger<AquisicaosController> _logger;

        public AquisicaosController(IAquisicaoService aquisicaoService, ILogger<AquisicaosController> logger)
        {
            _aquisicaoService = aquisicaoService;
            _logger = logger;
        }

        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpGet("mostrar-aquisicoes")]
        public async Task<ActionResult<IEnumerable<Aquisicao>>> GetAquisicaos()
        {
            var aquisicao = await _aquisicaoService.ObterTodosAsync();
            _logger.LogInformation("Aquisicoes listadas.");
            return Ok(aquisicao);
        }

        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpGet("mostrar-aquisicao-{id}")]
        public async Task<ActionResult<Aquisicao>> GetAquisicao(int id)
        {
            var aquisicao = await _aquisicaoService.ObterPorIdAsync(id);

            if (aquisicao == null)
            {
                _logger.LogWarning($"Tentativa de acessar aquisicao {id}, mas não foi encontrada.");
                return NotFound();
            }

            _logger.LogInformation($"Aquisicao {id} foi consultada.");
            return Ok(aquisicao);
        }

        [Authorize(Roles = "Administrador, Financeiro")]    
        [HttpPut("atualizar-{id}")]
        public async Task<IActionResult> UpdateAquisicao(int id, [FromBody] Aquisicao aquisicao)
        {
            if (id != aquisicao.IdAquisicao)
            {
                _logger.LogWarning($"Falha na atualização: ID no URL ({id}) não coincide com o ID no corpo ({aquisicao.IdAquisicao}).");
                return BadRequest("ID no URL e ID no objeto não coincidem.");
            }

            var updated = await _aquisicaoService.AtualizarAsync(aquisicao);

            if (!updated.Sucesso)
            {
                _logger.LogWarning($"Falha ao atualizar a aquisicao {id}: {updated.Mensagem}");
                return NotFound(new { mensagem = updated.Mensagem });
            }

            _logger.LogInformation($"Aquisicao {id} atualizada com sucesso.");
            return Ok(new { mensagem = updated.Mensagem });
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("apagar-{id}")]
        public async Task<IActionResult> DeleteAquisicao(int id)
        {
            var deleted = await _aquisicaoService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                _logger.LogWarning($"Falha ao apagar a aquisicao {id}: {deleted.Mensagem}");
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            _logger.LogInformation($"Aquisicao {id} apagada com sucesso.");
            return Ok(new { mensagem = deleted.Mensagem });
        }

        /// <summary>
        /// Processo de negócio Aquisicao
        /// </summary>
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPost("nova-aquisicao")]
        public async Task<IActionResult> PostAquisicao([FromBody] Aquisicao aquisicao)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Tentativa de criar aquisicao com modelo inválido.");
                return BadRequest(ModelState);
            }

            await _aquisicaoService.CriarAsync(aquisicao);
            _logger.LogInformation($"Nova aquisicao criada com ID {aquisicao.IdAquisicao}.");
            return CreatedAtAction(nameof(GetAquisicao), new { id = aquisicao.IdAquisicao }, aquisicao);
        }

        [Authorize(Roles = "Funcionário")]
        [HttpPost("submeter/{funcionarioId}")]
        public async Task<IActionResult> Submeter(int funcionarioId)
        {
            var resultado = await _aquisicaoService.SubmeterPedidoAsync(funcionarioId);
            if (!resultado.Sucesso)
            {
                _logger.LogWarning($"Falha ao submeter pedido de aquisicao pelo funcionário {funcionarioId}: {resultado.Mensagem}");
                return BadRequest(resultado.Mensagem);
            }

            _logger.LogInformation($"Funcionário {funcionarioId} submeteu um pedido de aquisicao.");
            return Ok(resultado.Mensagem);
        }

        [Authorize(Roles = "Financeiro")]
        [HttpPut("aprovar-financeiro/{id}")]
        public async Task<IActionResult> RejeitarFinanceiroAsync(int id)
        {
            var resultado = await _aquisicaoService.RejeitarFinanceiroAsync(id);
            if (!resultado.Sucesso)
            {
                _logger.LogWarning($"Financeiro falhou ao aprovar/rejeitar aquisicao {id}: {resultado.Mensagem}");
                return NotFound(resultado.Mensagem);
            }

            _logger.LogInformation($"Financeiro aprovou/rejeitou a aquisicao {id}.");
            return Ok(resultado.Mensagem);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("{aquisicaoId}/aprovar-admin")]
        public async Task<IActionResult> AprovarAdmin(int aquisicaoId, [FromBody] NovoVeiculoDTO dto)
        {
            var resultado = await _aquisicaoService.AprovarAdminAsync(aquisicaoId, dto);
            if (!resultado.Sucesso)
            {
                _logger.LogWarning($"Admin falhou ao aprovar a aquisicao {aquisicaoId}: {resultado.Mensagem}");
                return BadRequest(resultado.Mensagem);
            }

            _logger.LogInformation($"Admin aprovou a aquisicao {aquisicaoId} e criou novo veículo.");
            return Ok(resultado.Mensagem);
        }
    }
}
