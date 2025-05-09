using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FaiscaSync.Models;
using FaiscaSync.DTO;
using FaiscaSync.Services;
using Microsoft.AspNetCore.Authorization;

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly FuncionarioServices _service;

        public FuncionariosController(FuncionarioServices service)
        {
            _service = service;
        }

        // GET: api/Funcionarios
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet("pesquisar-funcionário")]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            var funcionarios = await _service.GetAllAsync();
            return Ok(funcionarios);
        }

        // GET: api/Funcionarios/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet("pesquisar-funcionario-{id}")]
        public async Task<ActionResult<Funcionario>> GetFuncionario(int id)
        {
            var funcionario = await _service.GetByIdAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }

        // PUT: api/Funcionarios/5
        [Authorize(Roles = "Administrador")]
        [HttpPut("atualizar-funcionario-{id}")]
        public async Task<IActionResult> PutFuncionario(int id, [FromBody] FuncionarioDTO funcionarioDTO)
        {
            var funcionario = new Funcionario
            {
                IdFuncionario = id,
                Nome = funcionarioDTO.Nome,
                Datacontratacao = funcionarioDTO.DataContrato,
                Datanascimento = funcionarioDTO.DataNasc,
                Contato = funcionarioDTO.Contato,
                Username = funcionarioDTO.Username,
                Password = funcionarioDTO.Password,
                IdCargo = funcionarioDTO.IdCargo
            };

            var updated = await _service.UpdateAsync(id, funcionario);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // POST: api/Funcionarios
        [Authorize(Roles = "Administrador")]
        [HttpPost("adicionar-funcionario")]
        public async Task<ActionResult<Funcionario>> PostFuncionario([FromBody] FuncionarioDTO funcionarioDTO)
        {
            var funcionario = new Funcionario
            {
                Nome = funcionarioDTO.Nome,
                Datacontratacao = funcionarioDTO.DataContrato,
                Datanascimento = funcionarioDTO.DataNasc,
                Contato = funcionarioDTO.Contato,
                Username = funcionarioDTO.Username,
                Password = funcionarioDTO.Password,
                IdCargo = funcionarioDTO.IdCargo
            };

            var created = await _service.CreateAsync(funcionario);
            return CreatedAtAction(nameof(GetFuncionario), new { id = created.IdFuncionario}, created);
        }

        // POST: api/Funcionarios/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var token = await _service.LoginAsync(loginRequestDTO);
            if (token == null)
                return Unauthorized(new { message = "Credenciais inválidas" });

            return Ok(new { token });
        }

        // DELETE: api/Funcionarios/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("eliminar-{id}")]
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
