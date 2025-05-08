using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using FaiscaSync.Services;
using Microsoft.AspNetCore.Authorization;
using FaiscaSync.DTO;
using Microsoft.Extensions.Logging;

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosVendaController : ControllerBase
    {
        private readonly IPosVendaService _posVendaService;
        private readonly ILogger<PosVendaController> _logger;

        public PosVendaController(IPosVendaService posVendaService, ILogger<PosVendaController> logger)
        {
            _posVendaService = posVendaService;
            _logger = logger;
        }

        // GET: api/PosVenda
        [Authorize(Roles = "Administrador")]
        [HttpGet("mostrar-todos-pos-vendas")]
        public async Task<ActionResult<IEnumerable<PosVenda>>> GetPosVenda()
        {
            _logger.LogInformation("[PosVendaController] Listagem de todas as solicitações de pós-venda iniciada.");
            var posVenda = await _posVendaService.ObterTodosAsync();
            return Ok(posVenda);
        }

        // GET: api/PosVenda/5
        [Authorize(Roles = "Administrador")]
        [HttpGet("mostrar-pos-venda-{id}")]
        public async Task<ActionResult<PosVenda>> GetPosVenda(int id)
        {
            _logger.LogInformation($"[PosVendaController] Consulta de solicitação de pós-venda com ID {id}.");
            var posVenda = await _posVendaService.ObterPorIdAsync(id);

            if (posVenda == null)
            {
                _logger.LogWarning($"[PosVendaController] Solicitação de pós-venda com ID {id} não encontrada.");
                return NotFound();
            }

            return Ok(posVenda);
        }

        // PUT: api/PosVenda/5
        [Authorize(Roles = "Administrador")]
        [HttpPut("atualizar-pos-venda-{id}")]
        public async Task<IActionResult> PutPosVenda(int id, [FromBody] PosVenda posVenda)
        {
            if (id != posVenda.IdPosVenda)
            {
                _logger.LogWarning($"[PosVendaController] Falha na atualização: ID no URL ({id}) e ID no objeto ({posVenda.IdPosVenda}) não coincidem.");
                return BadRequest("ID no URL e ID no objeto não coincidem.");
            }

            _logger.LogInformation($"[PosVendaController] Tentando atualizar solicitação de pós-venda ID {id}.");
            var updated = await _posVendaService.AtualizarAsync(posVenda);

            if (!updated.Sucesso)
            {
                _logger.LogWarning($"[PosVendaController] Falha na atualização da solicitação ID {id}: {updated.Mensagem}");
                return NotFound(new { mensagem = updated.Mensagem });
            }

            _logger.LogInformation($"[PosVendaController] Solicitação ID {id} atualizada com sucesso.");
            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/PosVenda
        [Authorize(Roles = "Administrador")]
        [HttpPost("criar-pos-venda")]
        public async Task<ActionResult<PosVenda>> PostPosVenda(PosVenda posVenda)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("[PosVendaController] Falha ao criar: ModelState inválido.");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("[PosVendaController] Criando nova solicitação de pós-venda.");
            await _posVendaService.CriarAsync(posVenda);
            _logger.LogInformation($"[PosVendaController] Solicitação criada com sucesso (ID: {posVenda.IdPosVenda}).");

            return CreatedAtAction(nameof(GetPosVenda), new { id = posVenda.IdPosVenda }, posVenda);
        }

        // DELETE: api/PosVenda/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("apagar-pos-venda-{id}")]
        public async Task<IActionResult> DeletePosVenda(int id)
        {
            _logger.LogInformation($"[PosVendaController] Tentando apagar solicitação de pós-venda ID {id}.");
            var deleted = await _posVendaService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                _logger.LogWarning($"[PosVendaController] Falha ao apagar solicitação ID {id}: {deleted.Mensagem}");
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            _logger.LogInformation($"[PosVendaController] Solicitação ID {id} apagada com sucesso.");
            return Ok(new { mensagem = deleted.Mensagem });
        }

        [HttpPost("criar-solicitacao")]
        public async Task<IActionResult> CriarSolicitacao([FromBody] CriarPosVendaDTO dto)
        {
            _logger.LogInformation($"[PosVendaController] Criando nova solicitação de pós-venda para Venda ID {dto.IdVendas}.");
            var resultado = await _posVendaService.CriarSolicitacaoAsync(dto);
            if (!resultado.Sucesso)
            {
                _logger.LogWarning($"[PosVendaController] Falha ao criar solicitação para Venda ID {dto.IdVendas}: {resultado.Mensagem}");
                return BadRequest(new { mensagem = resultado.Mensagem });
            }

            _logger.LogInformation($"[PosVendaController] Solicitação criada com sucesso para Venda ID {dto.IdVendas}.");
            return Ok(new { mensagem = resultado.Mensagem });
        }
    }
}
