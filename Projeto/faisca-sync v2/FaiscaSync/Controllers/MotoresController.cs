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
    public class MotoresController : ControllerBase
    {
        private readonly FsContext _context;

        public MotoresController(FsContext context)
        {
            _context = context;
        }

        // GET: api/Motores
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Motor>>> GetMotors()
        {
            return await _context.Motors.ToListAsync();
        }

        // GET: api/Motores/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Motor>> GetMotor(int id)
        {
            var motor = await _context.Motors.FindAsync(id);

            if (motor == null)
            {
                return NotFound();
            }

            return motor;
        }

        // PUT: api/Motores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMotor(int id, Motor motor)
        {
            if (id != motor.IdMotor)
            {
                return BadRequest();
            }

            _context.Entry(motor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotorExists(id))
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

        // POST: api/Motores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpPost]
        public async Task<ActionResult<Motor>> PostMotor(Motor motor)
        {
            _context.Motors.Add(motor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMotor", new { id = motor.IdMotor }, motor);
        }

        // DELETE: api/Motores/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotor(int id)
        {
            var motor = await _context.Motors.FindAsync(id);
            if (motor == null)
            {
                return NotFound();
            }

            _context.Motors.Remove(motor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MotorExists(int id)
        {
            return _context.Motors.Any(e => e.IdMotor == id);
        }
    }
}
