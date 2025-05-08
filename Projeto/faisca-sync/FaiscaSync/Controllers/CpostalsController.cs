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
    public class CpostalsController : ControllerBase
    {
        private readonly ICPostalService _cPostalService;

        public CpostalsController(ICPostalService cPostalService)
        {
            _cPostalService = cPostalService;
        }

        // GET: api/Cpostals
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostrar-codigos-postais")]
        public async Task<ActionResult<IEnumerable<Cpostal>>> GetCpostals()
        {
            var cPostal = await _cPostalService.ObterTodosAsync();
            return Ok(cPostal);
        }

        // GET: api/Cpostals/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostrar-codigo-postal-{id}")]
        public async Task<ActionResult<Cpostal>> GetCpostal(int id)
        {
            var cPostal = await _cPostalService.ObterPorIdAsync(id);

            if (cPostal == null)
            {
                return NotFound();
            }

            return Ok(cPostal);
        }

        // PUT: api/Cpostals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpPut("atualizar-codigo-postal-{id}")]
        public async Task<IActionResult> PutCpostal(int id, [FromBody]Cpostal cpostal)
        {
            if (id != cpostal.IdCpostal)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _cPostalService.AtualizarAsync(cpostal);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Cpostals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpPost("criar-codigo-postal")]
        public async Task<ActionResult<Cpostal>> PostCpostal([FromBody]Cpostal cpostal)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _cPostalService.CriarAsync(cpostal);
            return CreatedAtAction(nameof(GetCpostal), new { id = cpostal.IdCpostal }, cpostal);
        }

        // DELETE: api/Cpostals/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("apagar-codigo-postal-{id}")]
        public async Task<IActionResult> DeleteCpostal(int id)
        {
            var deleted = await _cPostalService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

    }
}
