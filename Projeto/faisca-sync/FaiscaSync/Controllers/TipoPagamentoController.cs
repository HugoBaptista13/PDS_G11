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
    public class TipoPagamentoController : ControllerBase
    {
        private readonly ITipoPagamentoService _tipoPagamentoService;

        public TipoPagamentoController(ITipoPagamentoService tipoPagamentoService)
        {
            _tipoPagamentoService = tipoPagamentoService;
        }

        // GET: api/TipoPagamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoPagamento>>> GetTipoPagamentos()
        {
            var tipoPagamento = await _tipoPagamentoService.ObterTodosAsync();
            return Ok(tipoPagamento);
        }

        // GET: api/TipoPagamento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoPagamento>> GetTipoPagamento(int id)
        {
            var tipoPagamento = await _tipoPagamentoService.ObterPorIdAsync(id);

            if (tipoPagamento == null)
            {
                return NotFound();
            }

            return Ok(tipoPagamento);
        }

        // PUT: api/TipoPagamento/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoPagamento(int id,[FromBody] TipoPagamento tipoPagamento)
        {
            if (id != tipoPagamento.IdTipoPagamento)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _tipoPagamentoService.AtualizarAsync(tipoPagamento);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/TipoPagamento
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoPagamento>> PostTipoPagamento(TipoPagamento tipoPagamento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _tipoPagamentoService.CriarAsync(tipoPagamento);
            return CreatedAtAction(nameof(GetTipoPagamento), new { id = tipoPagamento.IdTipoPagamento }, tipoPagamento);
        }

        // DELETE: api/TipoPagamento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoPagamento(int id)
        {
            var deleted = await _tipoPagamentoService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

        private async Task<ResultadoOperacao> TipoPagamentoExists(int id)
        {
            var tipoPagamento = await _tipoPagamentoService.ObterPorIdAsync(id);
            return tipoPagamento != null
                ? ResultadoOperacao.Ok("Tipo Pagamento encontrado.")
        : ResultadoOperacao.Falha("Tipo de Pagamento não encontrado.");
        }
    }
}
