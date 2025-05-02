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
    public class EstadoGarantiaController : ControllerBase
    {
        private readonly IEstadoGarantiaService _estadoGarantiumService;

        public EstadoGarantiaController(IEstadoGarantiaService estadoGarantiumService)
        {
            _estadoGarantiumService = estadoGarantiumService;
        }

        // GET: api/EstadoGarantia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoGarantia>>> GetEstadoGarantia()
        {
            var estadoGarantium = await _estadoGarantiumService.ObterTodosAsync();
            return Ok(estadoGarantium);
        }

        // GET: api/EstadoGarantia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoGarantia>> GetEstadoGarantium(int id)
        {
            var estadoGarantium = await _estadoGarantiumService.ObterPorIdAsync(id);

            if (estadoGarantium == null)
            {
                return NotFound();
            }

            return Ok(estadoGarantium);
        }

        // PUT: api/EstadoGarantia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoGarantium(int id,[FromBody] EstadoGarantia estadoGarantium)
        {
            if (id != estadoGarantium.IdEstadoGarantia)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _estadoGarantiumService.AtualizarAsync(estadoGarantium);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/EstadoGarantia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstadoGarantia>> PostEstadoGarantium([FromBody] EstadoGarantia estadoGarantium)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _estadoGarantiumService.CriarAsync(estadoGarantium);
            return CreatedAtAction(nameof(GetEstadoGarantia), new { id = estadoGarantium.IdEstadoGarantia }, estadoGarantium);
        }
        

        // DELETE: api/EstadoGarantia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadoGarantium(int id)
        {
            var deleted = await _estadoGarantiumService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

        private async Task<ResultadoOperacao> EstadoGarantiumExists(int id)
        {
            var estadoGarantia = await _estadoGarantiumService.ObterPorIdAsync(id);
            return estadoGarantia != null
                ? ResultadoOperacao.Ok("Aquisição encontrada.")
        : ResultadoOperacao.Falha("Aquisição não encontrada.");
        }
    }
}
