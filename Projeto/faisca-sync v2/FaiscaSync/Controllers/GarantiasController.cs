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
    public class GarantiasController : ControllerBase
    {
        private readonly FsContext _context;

        public GarantiasController(FsContext context)
        {
            _context = context;
        }

        // GET: api/Garantias
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Garantia>>> GetGarantia()
        {
            return await _context.Garantia.ToListAsync();
        }

        // GET: api/Garantias/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Garantia>> GetGarantium(int id)
        {
            var garantium = await _context.Garantia.FindAsync(id);

            if (garantium == null)
            {
                return NotFound();
            }

            return garantium;
        }

        // PUT: api/Garantias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGarantium(int id, Garantia garantium)
        {
            if (id != garantium.IdGarantia)
            {
                return BadRequest();
            }

            _context.Entry(garantium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GarantiumExists(id))
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

        // POST: api/Garantias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPost]
        public async Task<ActionResult<Garantia>> PostGarantium(Garantia garantium)
        {
            _context.Garantia.Add(garantium);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGarantium", new { id = garantium.IdGarantia }, garantium);
        }

        // DELETE: api/Garantias/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGarantium(int id)
        {
            var garantium = await _context.Garantia.FindAsync(id);
            if (garantium == null)
            {
                return NotFound();
            }

            _context.Garantia.Remove(garantium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GarantiumExists(int id)
        {
            return _context.Garantia.Any(e => e.IdGarantia == id);
        }
    }
}
