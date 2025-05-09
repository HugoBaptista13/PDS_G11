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
    public class CpostaisController : ControllerBase
    {
        private readonly FsContext _context;

        public CpostaisController(FsContext context)
        {
            _context = context;
        }

        // GET: api/Cpostais
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cpostal>>> GetCpostals()
        {
            return await _context.Cpostals.ToListAsync();
        }

        // GET: api/Cpostais/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Cpostal>> GetCpostal(int id)
        {
            var cpostal = await _context.Cpostals.FindAsync(id);

            if (cpostal == null)
            {
                return NotFound();
            }

            return cpostal;
        }

        // PUT: api/Cpostais/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCpostal(int id, Cpostal cpostal)
        {
            if (id != cpostal.IdCpostal)
            {
                return BadRequest();
            }

            _context.Entry(cpostal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CpostalExists(id))
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

        // POST: api/Cpostais
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        public async Task<ActionResult<Cpostal>> PostCpostal(Cpostal cpostal)
        {
            _context.Cpostals.Add(cpostal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCpostal", new { id = cpostal.IdCpostal }, cpostal);
        }

        // DELETE: api/Cpostais/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteCpostal(int id)
        {
            var cpostal = await _context.Cpostals.FindAsync(id);
            if (cpostal == null)
            {
                return NotFound();
            }

            _context.Cpostals.Remove(cpostal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CpostalExists(int id)
        {
            return _context.Cpostals.Any(e => e.IdCpostal == id);
        }
    }
}
