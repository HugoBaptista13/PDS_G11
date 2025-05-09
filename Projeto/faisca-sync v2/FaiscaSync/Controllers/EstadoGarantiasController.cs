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
    public class EstadoGarantiasController : ControllerBase
    {
        private readonly FsContext _context;

        public EstadoGarantiasController(FsContext context)
        {
            _context = context;
        }

        // GET: api/EstadosGarantias
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoGarantia>>> GetEstadoGarantia()
        {
            return await _context.EstadoGarantia.ToListAsync();
        }

        // GET: api/EstadosGarantias/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoGarantia>> GetEstadoGarantium(int id)
        {
            var estadoGarantium = await _context.EstadoGarantia.FindAsync(id);

            if (estadoGarantium == null)
            {
                return NotFound();
            }

            return estadoGarantium;
        }

        // PUT: api/EstadosGarantias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoGarantium(int id, EstadoGarantia estadoGarantium)
        {
            if (id != estadoGarantium.IdEstadoGarantia)
            {
                return BadRequest();
            }

            _context.Entry(estadoGarantium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoGarantiumExists(id))
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

        // POST: api/EstadosGarantias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<ActionResult<EstadoGarantia>> PostEstadoGarantium(EstadoGarantia estadoGarantium)
        {
            _context.EstadoGarantia.Add(estadoGarantium);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadoGarantium", new { id = estadoGarantium.IdEstadoGarantia }, estadoGarantium);
        }

        // DELETE: api/EstadosGarantias/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadoGarantium(int id)
        {
            var estadoGarantium = await _context.EstadoGarantia.FindAsync(id);
            if (estadoGarantium == null)
            {
                return NotFound();
            }

            _context.EstadoGarantia.Remove(estadoGarantium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadoGarantiumExists(int id)
        {
            return _context.EstadoGarantia.Any(e => e.IdEstadoGarantia == id);
        }
    }
}
