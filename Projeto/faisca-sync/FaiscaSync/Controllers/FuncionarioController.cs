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
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionarioController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        // GET: api/Funcionario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            var funcionario = await _funcionarioService.ObterTodosAsync();
            return Ok(funcionario);
        }

        // GET: api/Funcionario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> GetFuncionario(int id)
        {
            var funcionario = await _funcionarioService.ObterPorIdAsync(id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }

        // PUT: api/Funcionario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuncionario(int id, FuncionarioDTO funcionarioDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _funcionarioService.AtualizarAsync(id, funcionarioDto);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Funcionario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostFuncionario(FuncionarioDTO funcionarioDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var funcionario = new Funcionario
            {
                Nome = funcionarioDto.Nome,
                Datacontratacao = funcionarioDto.DataContrato,
                Datanascimento = funcionarioDto.DataNasc,
                IdCargo = funcionarioDto.IdCargo
            };

            await _funcionarioService.CriarAsync(funcionarioDto);
            return CreatedAtAction(nameof(GetFuncionario), new { id = funcionario.IdFuncionario }, funcionario);
        }

        // DELETE: api/Funcionario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            var deleted = await _funcionarioService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

        private async Task<ResultadoOperacao> FuncionarioExists(int id)
        {
            var funcionario = await _funcionarioService.ObterPorIdAsync(id);
            return funcionario != null
                ? ResultadoOperacao.Ok("Funcionário encontrado.")
        : ResultadoOperacao.Falha("Funcionário não encontrado.");
        }
    }
}
