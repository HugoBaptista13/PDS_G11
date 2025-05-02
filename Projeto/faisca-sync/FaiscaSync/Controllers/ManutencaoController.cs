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
    public class ManutencaoController : ControllerBase
    {
        private readonly IManutencaoService _manutencaoService;

        public ManutencaoController(IManutencaoService manutencaoService)
        {
            _manutencaoService = manutencaoService;
        }

        // GET: api/Manutencao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manutencao>>> GetManutencaos()
        {
            var manutencao = await _manutencaoService.ObterTodosAsync();
            return Ok(manutencao);
        }

        // GET: api/Manutencao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manutencao>> GetManutencao(int id)
        {
            var manutencao = await _manutencaoService.ObterPorIdAsync(id);

            if (manutencao == null)
            {
                return NotFound();
            }

            return Ok(manutencao);
        }

        // PUT: api/Manutencao/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManutencao(int id, [FromBody] Manutencao manutencao)
        {
            if (id != manutencao.IdManutencao)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _manutencaoService.AtualizarAsync(manutencao);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Manutencao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Manutencao>> PostManutencao([FromBody]Manutencao manutencao)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _manutencaoService.CriarAsync(manutencao);
            return CreatedAtAction(nameof(GetManutencao), new { id = manutencao.IdManutencao }, manutencao);
        }

        // DELETE: api/Manutencao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManutencao(int id)
        {
            var deleted = await _manutencaoService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

        private async Task<ResultadoOperacao> ManutencaoExists(int id)
        {
            var manutencao = await _manutencaoService.ObterPorIdAsync(id);
            return manutencao != null
                ? ResultadoOperacao.Ok("Manutenção encontrada.")
        : ResultadoOperacao.Falha("Manutenção não encontrada.");
        }
    }
}
