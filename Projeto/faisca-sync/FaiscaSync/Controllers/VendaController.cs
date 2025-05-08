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
using Microsoft.Extensions.Logging;

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly IVendaService _vendaService;
        private readonly ILogger<VendaController> _logger;

        public VendaController(IVendaService vendaService, ILogger<VendaController> logger)
        {
            _vendaService = vendaService;
            _logger = logger;
        }

        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpGet("mostrar-vendas")]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendas()
        {
            _logger.LogInformation("[Controller] Listagem de todas as vendas.");
            var vendas = await _vendaService.ObterTodosAsync();
            return Ok(vendas);
        }

        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpGet("mostrar-venda-{id}")]
        public async Task<ActionResult<Venda>> Getvenda(int id)
        {
            _logger.LogInformation($"[Controller] Consultando venda com ID {id}.");
            var venda = await _vendaService.ObterPorIdAsync(id);

            if (venda == null)
            {
                _logger.LogWarning($"[Controller] Venda {id} não encontrada.");
                return NotFound();
            }

            return Ok(venda);
        }

        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPut("atualizar-venda-{id}")]
        public async Task<IActionResult> UpdateVenda(int id, [FromBody] Venda venda)
        {
            if (id != venda.IdVendas)
            {
                _logger.LogWarning($"[Controller] ID no URL ({id}) não coincide com ID no corpo ({venda.IdVendas}).");
                return BadRequest("ID no URL e ID no objeto não coincidem.");
            }

            var updated = await _vendaService.AtualizarAsync(venda);

            if (!updated.Sucesso)
            {
                _logger.LogWarning($"[Controller] Falha ao atualizar venda {id}: {updated.Mensagem}");
                return NotFound(new { mensagem = updated.Mensagem });
            }

            _logger.LogInformation($"[Controller] Venda {id} atualizada com sucesso.");
            return Ok(new { mensagem = updated.Mensagem });
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("criar-venda")]
        public async Task<IActionResult> PostVenda([FromBody] Venda venda)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("[Controller] Tentativa de criar venda com modelo inválido.");
                return BadRequest(ModelState);
            }

            await _vendaService.CriarAsync(venda);
            _logger.LogInformation($"[Controller] Venda criada com sucesso (ID {venda.IdVendas}).");
            return CreatedAtAction(nameof(Getvenda), new { id = venda.IdVendas }, venda);
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("apagar-venda-{id}")]
        public async Task<IActionResult> DeleteVenda(int id)
        {
            var deleted = await _vendaService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                _logger.LogWarning($"[Controller] Falha ao apagar venda {id}: {deleted.Mensagem}");
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            _logger.LogInformation($"[Controller] Venda {id} apagada com sucesso.");
            return Ok(new { mensagem = deleted.Mensagem });
        }

        [Authorize(Roles = "Funcionario")]
        [HttpPut("reservar/{veiculoId}/{funcionarioId}")]
        public async Task<IActionResult> ReservarVeiculo(int veiculoId, int funcionarioId)
        {
            _logger.LogInformation($"[Controller] Funcionário {funcionarioId} iniciou reserva para Veículo {veiculoId}.");
            var resultado = await _vendaService.ReservarVeiculoAsync(veiculoId, funcionarioId);
            if (!resultado.Sucesso)
            {
                _logger.LogWarning($"[Controller] Falha na reserva do Veículo {veiculoId}: {resultado.Mensagem}");
                return BadRequest(resultado.Mensagem);
            }
            return Ok(resultado.Mensagem);
        }

        [Authorize(Roles = "Funcionário")]
        [HttpPost("submeter-venda/{veiculoId}/{funcionarioId}")]
        public async Task<IActionResult> SubmeterVenda(int veiculoId, int funcionarioId)
        {
            _logger.LogInformation($"[Controller] Funcionário {funcionarioId} está a submeter venda para Veículo {veiculoId}.");
            var resultado = await _vendaService.SubmeterVendaAsync(veiculoId, funcionarioId);
            if (!resultado.Sucesso)
            {
                _logger.LogWarning($"[Controller] Falha na submissão da venda para Veículo {veiculoId}: {resultado.Mensagem}");
                return BadRequest(resultado.Mensagem);
            }
            return Ok(resultado.Mensagem);
        }

        [Authorize(Roles = "Financeiro")]
        [HttpPut("aprovar-financeiro/{vendaId}")]
        public async Task<IActionResult> AprovarFinanceiro(int vendaId)
        {
            _logger.LogInformation($"[Controller] Iniciando aprovação financeira para Venda {vendaId}.");
            var resultado = await _vendaService.AprovarFinanceiroAsync(vendaId);
            if (!resultado.Sucesso)
            {
                _logger.LogWarning($"[Controller] Falha na aprovação financeira da venda {vendaId}: {resultado.Mensagem}");
                return BadRequest(resultado.Mensagem);
            }
            return Ok(resultado.Mensagem);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("finalizar-venda/{vendaId}")]
        public async Task<IActionResult> FinalizarVenda(int vendaId)
        {
            var funcionarioIdClaim = User.Claims.FirstOrDefault(c => c.Type == "IdFuncionario");
            if (funcionarioIdClaim == null)
            {
                _logger.LogWarning("[Controller] Tentativa de finalização sem IdFuncionario válido no token.");
                return Unauthorized("Não foi possível identificar o funcionário.");
            }

            var funcionarioId = int.Parse(funcionarioIdClaim.Value);

            _logger.LogInformation($"[Controller] Finalizando Venda {vendaId} pelo Funcionário {funcionarioId}.");
            var resultado = await _vendaService.FinalizarVendaAdminAsync(vendaId, funcionarioId);

            if (!resultado.Sucesso)
            {
                _logger.LogWarning($"[Controller] Falha na finalização da venda {vendaId}: {resultado.Mensagem}");
                return BadRequest(resultado.Mensagem);
            }

            return Ok(resultado.Mensagem);
        }
    }
}
