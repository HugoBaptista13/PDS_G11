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
    public class ManutencoesController : ControllerBase
    {
        private readonly FsContext _context;

        public ManutencoesController(FsContext context)
        {
            _context = context;
        }

        // GET: api/Manutencoes
        [Authorize(Roles = "Administrador, Financeiro, Funcionario, Mecanico")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manutencao>>> GetManutencaos()
        {
            return await _context.Manutencaos.ToListAsync();
        }

        // GET: api/Manutencoes/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionario, Mecanico")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Manutencao>> GetManutencao(int id)
        {
            var manutencao = await _context.Manutencaos.FindAsync(id);

            if (manutencao == null)
            {
                return NotFound();
            }

            return manutencao;
        }

        // PUT: api/Manutencoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManutencao(int id, Manutencao manutencao)
        {
            if (id != manutencao.IdManutencao)
            {
                return BadRequest();
            }

            _context.Entry(manutencao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManutencaoExists(id))
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

        // POST: api/Manutencoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpPost]
        public async Task<ActionResult<Manutencao>> PostManutencao(Manutencao manutencao)
        {
            _context.Manutencaos.Add(manutencao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetManutencao", new { id = manutencao.IdManutencao }, manutencao);
        }

        // DELETE: api/Manutencoes/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManutencao(int id)
        {
            var manutencao = await _context.Manutencaos.FindAsync(id);
            if (manutencao == null)
            {
                return NotFound();
            }

            _context.Manutencaos.Remove(manutencao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ManutencaoExists(int id)
        {
            return _context.Manutencaos.Any(e => e.IdManutencao == id);
        }
    }
}
