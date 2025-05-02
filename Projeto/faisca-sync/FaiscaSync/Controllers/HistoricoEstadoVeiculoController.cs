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

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoEstadoVeiculoController : ControllerBase
    {
        private readonly IHistoricoEstadoVeiculoService _historicoEstadoVeiculoService;

        public HistoricoEstadoVeiculoController(IHistoricoEstadoVeiculoService historicoEstadoVeiculoService)
        {
            _historicoEstadoVeiculoService = historicoEstadoVeiculoService;
        }

        // GET: api/HistoricoEstadoVeiculo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoricoEstadoVeiculo>>> GetHistoricoEstadoVeiculos()
        {
            var historicoEstadoVeiculos = await _historicoEstadoVeiculoService.ObterTodosAsync();
            return Ok(historicoEstadoVeiculos);
        }

        // GET: api/HistoricoEstadoVeiculo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoricoEstadoVeiculo>> GetHistoricoEstadoVeiculo(int id)
        {
            var historicoEstadoVeiculo = await _historicoEstadoVeiculoService.ObterPorIdAsync(id);

            if (historicoEstadoVeiculo == null)
            {
                return NotFound();
            }

            return Ok(historicoEstadoVeiculo);
        }

        // PUT: api/HistoricoEstadoVeiculo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoricoEstadoVeiculo(int id, [FromBody] HistoricoEstadoVeiculo historicoEstadoVeiculo)
        {
            if (id != historicoEstadoVeiculo.IdHistEstadoVeiculo)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _historicoEstadoVeiculoService.AtualizarAsync(historicoEstadoVeiculo);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/HistoricoEstadoVeiculo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistoricoEstadoVeiculo>> PostHistoricoEstadoVeiculo([FromBody] HistoricoEstadoVeiculo historicoEstadoVeiculo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _historicoEstadoVeiculoService.CriarAsync(historicoEstadoVeiculo);
            return CreatedAtAction(nameof(GetHistoricoEstadoVeiculo), new { id = historicoEstadoVeiculo.IdHistEstadoVeiculo }, historicoEstadoVeiculo);
        }

        // DELETE: api/HistoricoEstadoVeiculo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoricoEstadoVeiculo(int id)
        {
            var deleted = await _historicoEstadoVeiculoService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

        private async Task<ResultadoOperacao> HistoricoEstadoVeiculoExists(int id)
        {
            var historicoEstadoVeiculo = await _historicoEstadoVeiculoService.ObterPorIdAsync(id);
            return historicoEstadoVeiculo != null
                ? ResultadoOperacao.Ok("Histórico Estado Veiculo encontrado.")
        : ResultadoOperacao.Falha("Histórico Estado Veiculo não encontrado.");
        }
    }
}
