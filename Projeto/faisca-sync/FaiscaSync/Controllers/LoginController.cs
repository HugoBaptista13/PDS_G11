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
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILoginService loginService, ILogger<LoginController> logger)
        {
            _loginService = loginService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("autenticar-login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("[Login] Tentativa de login com modelo inválido.");
                return BadRequest(ModelState);
            }

            var token = await _loginService.AutenticarAsync(loginDto.Username, loginDto.Password);

            if (token == null)
            {
                _logger.LogWarning($"[Login] Falha na autenticação para o utilizador {loginDto.Username}.");
                return Unauthorized(new { message = "Username ou password incorretos." });
            }

            _logger.LogInformation($"[Login] Utilizador {loginDto.Username} autenticado com sucesso.");
            return Ok(new { token });
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("criar-login")]
        public async Task<IActionResult> CriarLogin([FromBody] NovoLoginDTO novoLogin)
        {
            _logger.LogInformation($"[Admin] Criando novo login para o utilizador: {novoLogin.username}.");

            var resultado = await _loginService.CriarLoginAsync(novoLogin);

            if (!resultado.Sucesso)
            {
                _logger.LogWarning($"[Admin] Falha ao criar login para {novoLogin.username}: {resultado.Mensagem}");
                return BadRequest(new { mensagem = resultado.Mensagem });
            }

            _logger.LogInformation($"[Admin] Login criado com sucesso para {novoLogin.username}.");
            return Ok(new { mensagem = resultado.Mensagem });
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("mostrar-todos-logins")]
        public async Task<ActionResult<IEnumerable<Login>>> GetLogins()
        {
            _logger.LogInformation("[Admin] Listando todos os logins.");
            var logins = await _loginService.GetAllLoginsAsync();
            return Ok(logins);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("mostrar-{id}")]
        public async Task<ActionResult<Login>> GetLogin(int id)
        {
            _logger.LogInformation($"[Admin] Buscando login com ID {id}.");
            var login = await _loginService.GetLoginByIdAsync(id);

            if (login == null)
            {
                _logger.LogWarning($"[Admin] Login com ID {id} não encontrado.");
                return NotFound();
            }

            return Ok(login);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("atualizar-{id}")]
        public async Task<IActionResult> UpdateLogin(int id, [FromBody] Login login)
        {
            if (id != login.IdLogin)
            {
                _logger.LogWarning($"[Admin] ID no URL ({id}) não coincide com ID no objeto ({login.IdLogin}).");
                return BadRequest("ID no URL e ID no objeto não coincidem.");
            }

            _logger.LogInformation($"[Admin] Atualizando login com ID {id}.");
            var updated = await _loginService.UpdateLoginAsync(login);

            if (!updated.Sucesso)
            {
                _logger.LogWarning($"[Admin] Falha ao atualizar login {id}: {updated.Mensagem}");
                return NotFound(new { mensagem = updated.Mensagem });
            }

            _logger.LogInformation($"[Admin] Login {id} atualizado com sucesso.");
            return Ok(new { mensagem = updated.Mensagem });
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("apagar-{id}")]
        public async Task<IActionResult> DeleteLogin(int id)
        {
            _logger.LogInformation($"[Admin] Tentando apagar login com ID {id}.");
            var deleted = await _loginService.RemoverAsync(id);

            if (!deleted.Sucesso)
            {
                _logger.LogWarning($"[Admin] Falha ao apagar login {id}: {deleted.Mensagem}");
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            _logger.LogInformation($"[Admin] Login {id} apagado com sucesso.");
            return Ok(new { mensagem = deleted.Mensagem });
        }
    }
}
