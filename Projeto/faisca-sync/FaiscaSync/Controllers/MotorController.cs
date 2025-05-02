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
    public class MotorController : ControllerBase
    {
        private readonly IMotorService _motorService;

        public MotorController(IMotorService motorService)
        {
            _motorService = motorService;
        }

        // GET: api/Motor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Motor>>> GetMotors()
        {
            var motor = await _motorService.ObterTodosAsync();
            return Ok(motor);
        }

        // GET: api/Motor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Motor>> GetMotor(int id)
        {
            var motor = await _motorService.ObterPorIdAsync(id);

            if (motor == null)
            {
                return NotFound();
            }

            return Ok(motor);
        }

        // PUT: api/Motor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMotor(int id,[FromBody] Motor motor)
        {
            if (id != motor.IdMotor)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _motorService.AtualizarAsync(motor);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Motor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Motor>> PostMotor([FromBody]Motor motor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _motorService.CriarAsync(motor);
            return CreatedAtAction(nameof(GetMotor), new { id = motor.IdMotor }, motor);
        }

        // DELETE: api/Motor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotor(int id)
        {
            var deleted = await _motorService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

        private async Task<ResultadoOperacao> MotorExists(int id)
        {
            var motor = await _motorService.ObterPorIdAsync(id);
            return motor != null
                ? ResultadoOperacao.Ok("Motor encontrado.")
        : ResultadoOperacao.Falha("Motor não encontrado.");
        }
    }
}
