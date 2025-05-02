using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using FaiscaSync.DTO;
using FaiscaSync.Services;
using Microsoft.AspNetCore.Identity.Data;

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        // POST: api/Login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _loginService.AutenticarAsync(loginDto.Username, loginDto.Password);

            if (token == null)
                return Unauthorized(new { message = "Username ou password incorretos." });

            return Ok(new { token });
        }

        // GET: api/Login (listar todos os logins)
        [HttpGet("autorizado")]
        public async Task<ActionResult<IEnumerable<Login>>> GetLogins()
        {
            var logins = await _loginService.GetAllLoginsAsync();
            return Ok(logins);
        }

        // GET: api/Login/5 (obter login por ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Login>> GetLogin(int id)
        {
            var login = await _loginService.GetLoginByIdAsync(id);

            if (login == null)
                return NotFound();

            return Ok(login);
        }

        // PUT: api/Login/5 (atualizar login)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLogin(int id, [FromBody] Login login)
        {
            if (id != login.IdLogin)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _loginService.UpdateLoginAsync(login);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });
            return Ok(new { mensagem = updated.Mensagem });
        }

        // DELETE: api/Login/5 (apagar login)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogin(int id)
        {
            var deleted = await _loginService.RemoverAsync(id);

            if (!deleted.Sucesso)
                return NotFound(new { mensagem = deleted.Mensagem });

            return NotFound(new { mensagem = deleted.Mensagem });
        }
        private async Task<ResultadoOperacao> LoginExists(int id)
        {
            var login = await _loginService.GetLoginByIdAsync(id);
            return login != null
                 ? ResultadoOperacao.Ok("Login encontrado.")
        : ResultadoOperacao.Falha("Login não encontrado.");
        }


    }
}
