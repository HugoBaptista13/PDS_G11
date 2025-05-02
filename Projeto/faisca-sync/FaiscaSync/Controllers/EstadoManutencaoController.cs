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
    public class EstadoManutencaoController : ControllerBase
    {
        private readonly IEstadoManutencaoService _estadoManutencaoService;

        public EstadoManutencaoController(IEstadoManutencaoService estadoManutencaoService)
        {
            _estadoManutencaoService = estadoManutencaoService;
        }

        // GET: api/EstadoManutencao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoManutencao>>> GetEstadoManutencaos()
        {
            var estadoManutencao = await _estadoManutencaoService.ObterTodosAsync();
            return Ok(estadoManutencao);
        }

        // GET: api/EstadoManutencao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoManutencao>> GetEstadoManutencao(int id)
        {
            var estadoManutencao = await _estadoManutencaoService.ObterPorIdAsync(id);

            if (estadoManutencao == null)
            {
                return NotFound();
            }

            return Ok(estadoManutencao);
        }

        // PUT: api/EstadoManutencao/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoManutencao(int id, [FromBody]EstadoManutencao estadoManutencao)
        {
            if (id != estadoManutencao.IdEstadoManutencao)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _estadoManutencaoService.AtualizarAsync(estadoManutencao);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/EstadoManutencao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstadoManutencao>> PostEstadoManutencao([FromBody]EstadoManutencao estadoManutencao)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _estadoManutencaoService.CriarAsync(estadoManutencao);
            return CreatedAtAction(nameof(GetEstadoManutencao), new { id = estadoManutencao.IdEstadoManutencao }, estadoManutencao);
        }

        // DELETE: api/EstadoManutencao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadoManutencao(int id)
        {
            var deleted = await _estadoManutencaoService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

        private async Task<ResultadoOperacao> EstadoManutencaoExists(int id)
        {
            var estadoManutencao = await _estadoManutencaoService.ObterPorIdAsync(id);
            return estadoManutencao != null
                ? ResultadoOperacao.Ok("Estado Manutenção encontrado.")
        : ResultadoOperacao.Falha("Estado Manutenção não encontrado.");
        }
    }
}
