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
    public class GarantiaController : ControllerBase
    {
        private readonly IGarantiaService _garantiaService;

        public GarantiaController(IGarantiaService garantiaService)
        {
            _garantiaService = garantiaService;
        }

        // GET: api/Garantia
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpGet("mostrar-todas-garantias")]
        public async Task<ActionResult<IEnumerable<Garantia>>> GetGarantia()
        {
            var garantia = await _garantiaService.ObterTodosAsync();
            return Ok(garantia);
        }

        // GET: api/Garantia/5
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpGet("mostrar-garantia-{id}")]
        public async Task<ActionResult<Garantia>> GetGarantia(int id)
        {
            var garantia = await _garantiaService.ObterPorIdAsync(id);

            if (garantia == null)
            {
                return NotFound();
            }

            return Ok(garantia);
        }

        // PUT: api/Garantia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPut("atualizar-garantia-{id}")]
        public async Task<IActionResult> PutGarantia(int id, [FromBody]Garantia garantia)
        {
            if (id != garantia.IdGarantia)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _garantiaService.AtualizarAsync(garantia);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Garantia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPost("criar-garantia")]
        public async Task<ActionResult<Garantia>> PostGarantia(Garantia garantia)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _garantiaService.CriarAsync(garantia);
            return CreatedAtAction(nameof(GetGarantia), new { id = garantia.IdGarantia }, garantia);
        }

        // DELETE: api/Garantia/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("apagar-garantia-{id}")]
        public async Task<IActionResult> DeleteGarantia(int id)
        {
            var deleted = await _garantiaService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

    }
}
