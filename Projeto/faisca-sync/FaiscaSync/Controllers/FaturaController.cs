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
    public class FaturaController : ControllerBase
    {
        private readonly IFaturaService _faturaService;

        public FaturaController(IFaturaService faturaService)
        {
            _faturaService = faturaService;
        }

        // GET: api/Fatura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fatura>>> GetFaturas()
        {
            var fatura = await _faturaService.ObterTodosAsync();
            return Ok(fatura);
        }

        // GET: api/Fatura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fatura>> GetFatura(int id)
        {
            var fatura = await _faturaService.ObterPorIdAsync(id);

            if (fatura == null)
            {
                return NotFound();
            }

            return Ok(fatura);
        }

        // PUT: api/Fatura/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFatura(int id, Fatura fatura)
        {
            if (id != fatura.IdFatura)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _faturaService.AtualizarAsync(fatura);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Fatura
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fatura>> PostFatura(Fatura fatura)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _faturaService.CriarAsync(fatura);
            return CreatedAtAction(nameof(GetFatura), new { id = fatura.IdFatura }, fatura);
        }

        // DELETE: api/Fatura/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFatura(int id)
        {
            var deleted = await _faturaService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

        private async Task<ResultadoOperacao> FaturaExists(int id)
        {
            var fatura = await _faturaService.ObterPorIdAsync(id);
            return fatura != null
                ? ResultadoOperacao.Ok("Fatura foi encontrada.")
        : ResultadoOperacao.Falha("Fatura não encontrada.");
        }
    }
}
