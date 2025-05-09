using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FaiscaSync.Models;
using Microsoft.AspNetCore.Authorization;

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoManutencoesController : ControllerBase
    {
        private readonly FsContext _context;

        public EstadoManutencoesController(FsContext context)
        {
            _context = context;
        }

        // GET: api/EstadoManutencoes
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoManutencao>>> GetEstadoManutencaos()
        {
            return await _context.EstadoManutencaos.ToListAsync();
        }

        // GET: api/EstadoManutencoes/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoManutencao>> GetEstadoManutencao(int id)
        {
            var estadoManutencao = await _context.EstadoManutencaos.FindAsync(id);

            if (estadoManutencao == null)
            {
                return NotFound();
            }

            return estadoManutencao;
        }

        // PUT: api/EstadoManutencoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoManutencao(int id, EstadoManutencao estadoManutencao)
        {
            if (id != estadoManutencao.IdEstadoManutencao)
            {
                return BadRequest();
            }

            _context.Entry(estadoManutencao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoManutencaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EstadoManutencoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<ActionResult<EstadoManutencao>> PostEstadoManutencao(EstadoManutencao estadoManutencao)
        {
            _context.EstadoManutencaos.Add(estadoManutencao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadoManutencao", new { id = estadoManutencao.IdEstadoManutencao }, estadoManutencao);
        }

        // DELETE: api/EstadoManutencoes/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadoManutencao(int id)
        {
            var estadoManutencao = await _context.EstadoManutencaos.FindAsync(id);
            if (estadoManutencao == null)
            {
                return NotFound();
            }

            _context.EstadoManutencaos.Remove(estadoManutencao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadoManutencaoExists(int id)
        {
            return _context.EstadoManutencaos.Any(e => e.IdEstadoManutencao == id);
        }
    }
}
