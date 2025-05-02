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
    public class PosVendaController : ControllerBase
    {
        private readonly IPosVendaService _posVendaService;

        public PosVendaController(IPosVendaService posVendaService)
        {
            _posVendaService = posVendaService;
        }

        // GET: api/PosVenda
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosVenda>>> GetPosVenda()
        {
            var posVenda = await _posVendaService.ObterTodosAsync();
            return Ok(posVenda);
        }

        // GET: api/PosVenda/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PosVenda>> GetPosVenda(int id)
        {
            var posVenda = await _posVendaService.ObterPorIdAsync(id);

            if (posVenda == null)
            {
                return NotFound();
            }

            return Ok(posVenda);
        }

        // PUT: api/PosVenda/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosVenda(int id,[FromBody] PosVenda posVenda)
        {
            if (id != posVenda.IdPosVenda)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _posVendaService.AtualizarAsync(posVenda);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/PosVenda
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PosVenda>> PostPosVenda(PosVenda posVenda)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _posVendaService.CriarAsync(posVenda);
            return CreatedAtAction(nameof(GetPosVenda), new { id = posVenda.IdPosVenda }, posVenda);
        }

        // DELETE: api/PosVenda/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosVenda(int id)
        {
            var deleted = await _posVendaService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

        private async Task<ResultadoOperacao> PosVendaExists(int id)
        {
            var posVenda = await _posVendaService.ObterPorIdAsync(id);
            return posVenda != null
                ? ResultadoOperacao.Ok("Pos Venda encontrada.")
        : ResultadoOperacao.Falha("Pos Venda não encontrada.");
        }
    }
}
