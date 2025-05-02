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
    public class ContactosController : ControllerBase
    {
        private readonly IContatoService _contactoService;

        public ContactosController(IContatoService contatoService)
        {
            _contactoService = contatoService;
        }

        // GET: api/Contactos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contato>>> GetContatos()
        {
            var contato = await _contactoService.ObterTodosAsync();
            return Ok(contato);
        }

        // GET: api/Contactos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contato>> GetContato(int id)
        {
            var contato = await _contactoService.ObterPorIdAsync(id);

            if (contato == null)
            {
                return NotFound();
            }

            return Ok(contato);
        }

        // PUT: api/Contactos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContato(int id, [FromBody]Contato contato)
        {
            if (id != contato.Idcontato)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _contactoService.AtualizarAsync(contato);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Contactos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contato>> PostContato(Contato contato)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _contactoService.CriarAsync(contato);
            return CreatedAtAction(nameof(GetContato), new { id = contato.Idcontato }, contato);
        }

        // DELETE: api/Contactos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContato(int id)
        {
            var deleted = await _contactoService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

        private async Task<ResultadoOperacao> ContatoExists(int id)
        {
            var contato = await _contactoService.ObterPorIdAsync(id);
            return contato != null
                ? ResultadoOperacao.Ok("Aquisição encontrada.")
        : ResultadoOperacao.Falha("Aquisição não encontrada.");
        }
    }
}
