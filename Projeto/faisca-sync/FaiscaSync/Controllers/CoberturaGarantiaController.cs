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
    public class CoberturaGarantiaController : ControllerBase
    {
        private readonly ICoberturaGarantiaService _coberturagarantiaService;

        public CoberturaGarantiaController(ICoberturaGarantiaService coberturaGarantiaService)
        {
            _coberturagarantiaService = coberturaGarantiaService;
        }

        // GET: api/CoberturaGarantia
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostrar-coberturas-garantia")]
        public async Task<ActionResult<IEnumerable<CoberturaGarantia>>> GetCoberturaGarantium()
        {
            var coberturaGarantium = await _coberturagarantiaService.ObterTodosAsync();
            return Ok(coberturaGarantium);
        }

        // GET: api/CoberturaGarantia/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostrar-cobertura-garantia-{id}")]
        public async Task<ActionResult<CoberturaGarantia>> GetCoberturaGarantium(int id)
        {
            var coberturaGarantium = await _coberturagarantiaService.ObterPorIdAsync(id);

            if (coberturaGarantium == null)
            {
                return NotFound();
            }

            return Ok(coberturaGarantium);
        }

        // PUT: api/CoberturaGarantia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPut("atualizar-cobertura-{id}")]
        public async Task<IActionResult> PutCoberturaGarantium(int id, [FromBody] CoberturaGarantia coberturaGarantium)
        {
            if (id != coberturaGarantium.IdCoberturaGarantia)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _coberturagarantiaService.AtualizarAsync(coberturaGarantium);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/CoberturaGarantia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPost ("Criar Cobertura Garantia")]
        public async Task<ActionResult<CoberturaGarantia>> PostCoberturaGarantium(CoberturaGarantia coberturaGarantium)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _coberturagarantiaService.CriarAsync(coberturaGarantium);
            return CreatedAtAction(nameof(GetCoberturaGarantium), new { id = coberturaGarantium.IdCoberturaGarantia }, coberturaGarantium);
        }

        // DELETE: api/CoberturaGarantia/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("apagar-cobertura-{id}")]
        public async Task<IActionResult> DeleteCoberturaGarantium(int id)
        {
            var deleted = await _coberturagarantiaService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

    }
}
