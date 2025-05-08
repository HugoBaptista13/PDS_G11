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
    public class MoradaController : ControllerBase
    {
        private readonly IMoradaService _moradaService;

        public MoradaController(IMoradaService moradaService)
        {
            _moradaService = moradaService;
        }

        // GET: api/Morada
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostrar-todas-moradas")]
        public async Task<ActionResult<IEnumerable<Morada>>> GetMorada()
        {
            var morada = await _moradaService.ObterTodosAsync();
            return Ok(morada);       
        }

        // GET: api/Morada/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostrar-morada-{id}")]
        public async Task<ActionResult<Morada>> GetMorada(int id)
        {
            var morada = await _moradaService.ObterPorIdAsync(id);

            if (morada == null)
            {
                return NotFound();
            }

            return Ok(morada);
        }

        // PUT: api/Morada/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPut("atualizar-morada{id}")]
        public async Task<IActionResult> PutMoradum(int id, [FromBody]Morada morada)
        {
            if (id != morada.IdMorada)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _moradaService.AtualizarAsync(morada);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Morada
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpPost("criar-morada")]
        public async Task<ActionResult<Morada>> PostMorada([FromBody]Morada morada)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _moradaService.CriarAsync(morada);
            return CreatedAtAction(nameof(GetMorada), new { id = morada.IdMorada }, morada);
        }

        // DELETE: api/Morada/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpDelete("eliminar-morada-{id}")]
        public async Task<IActionResult> DeleteMorada(int id)
        {
            var deleted = await _moradaService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

    }
}
