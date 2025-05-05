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
using FaiscaSync.DTO;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cpostal>>> GetCpostals()
        {
            var cPostal = await _cPostalService.ObterTodosAsync();
            return Ok(cPostal);
        }

        // GET: api/Cpostals/5
        [HttpGet("{id}")]
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCpostal(int id, [FromBody]CpostalDTO cpostalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _cPostalService.AtualizarAsync(id, cpostalDto);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Cpostals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cpostal>> PostCpostal([FromBody] CpostalDTO cpostalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Cria o objeto no banco de dados
            var cpostal = new Cpostal
            {
                Localidade = cpostalDto.Localidade
            };

            await _cPostalService.CriarAsync(cpostalDto);

            // Retorna o objeto criado com o ID gerado
            return CreatedAtAction(nameof(GetCpostal), new { id = cpostal.IdCpostal }, cpostal);
        }

        // DELETE: api/Cpostals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCpostal(int id)
        {
            var deleted = await _cPostalService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

        private async Task<ResultadoOperacao> CpostalExists(int id)
        {
            var cpostal = await _cPostalService.ObterPorIdAsync(id);
            return cpostal != null
                ? ResultadoOperacao.Ok("Código Postal encontrado.")
        : ResultadoOperacao.Falha("Código Postal não encontrado.");
        }
    }
}
