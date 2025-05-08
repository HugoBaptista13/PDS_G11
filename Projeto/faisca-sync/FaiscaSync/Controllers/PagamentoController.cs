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

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly IPagamentoService _pagamentoService;

        public PagamentoController(IPagamentoService pagamentoService)
        {
            _pagamentoService = pagamentoService;
        }

        // GET: api/Pagamento
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpGet("mostrar-todos-pagamentos")]
        public async Task<ActionResult<IEnumerable<Pagamento>>> GetPagamentos()
        {
            var pagamento = await _pagamentoService.ObterTodosAsync();
            return Ok(pagamento);
        }

        // GET: api/Pagamento/5
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpGet("mostrar-pagamento-{id}")]
        public async Task<ActionResult<Pagamento>> GetPagamento(int id)
        {
            var pagamento = await _pagamentoService.ObterPorIdAsync(id);

            if (pagamento == null)
            {
                return NotFound();
            }

            return Ok(pagamento);
        }

        // PUT: api/Pagamento/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPut("atualizar-pagamento-{id}")]
        public async Task<IActionResult> PutPagamento(int id,[FromBody] Pagamento pagamento)
        {
            if (id != pagamento.IdPagamento)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _pagamentoService.AtualizarAsync(pagamento);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Pagamento
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPost("criar-pagamento")]
        public async Task<ActionResult<Pagamento>> PostPagamento([FromBody]Pagamento pagamento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _pagamentoService.CriarAsync(pagamento);
            return CreatedAtAction(nameof(GetPagamento), new { id = pagamento.IdPagamento }, pagamento);
        }

        // DELETE: api/Pagamento/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("apagar-pagamento-{id}")]
        public async Task<IActionResult> DeletePagamento(int id)
        {
            var deleted = await _pagamentoService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

    }
}
