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
    public class MoradaController : ControllerBase
    {
        private readonly IMoradaService _moradaService;

        public MoradaController(IMoradaService moradaService)
        {
            _moradaService = moradaService;
        }

        // GET: api/Morada
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Morada>>> GetMorada()
        {
            var morada = await _moradaService.ObterTodosAsync();
            return Ok(morada);       
        }

        // GET: api/Morada/5
        [HttpGet("{id}")]
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoradum(int id, [FromBody]MoradaDTO moradaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _moradaService.AtualizarAsync(id, moradaDto);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Morada
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Morada>> PostMorada([FromBody]MoradaDTO moradaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var morada = new Morada
            {
                Rua = moradaDto.Rua,
                Numero = moradaDto.Numero,
                Descricaomorada = moradaDto.Descricaomorada,
                Pais = moradaDto.Pais,
                IdCpostal = moradaDto.IdCpostal
            };

            await _moradaService.CriarAsync(moradaDto);
            return CreatedAtAction(nameof(GetMorada), new { id = morada.IdMorada }, morada);
        }

        // DELETE: api/Morada/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMorada(int id)
        {
            var deleted = await _moradaService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

        private async Task<ResultadoOperacao> MoradaExists(int id){
        
            var morada = await _moradaService.ObterPorIdAsync(id);
            return  morada != null
                ? ResultadoOperacao.Ok("Morada encontrada.")
        : ResultadoOperacao.Falha("Morada não encontrada.");
        }
    }
}
