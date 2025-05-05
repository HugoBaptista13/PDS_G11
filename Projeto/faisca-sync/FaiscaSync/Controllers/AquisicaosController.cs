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
using NuGet.Protocol.Plugins;
using FaiscaSync.DTO;

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AquisicaosController : ControllerBase
    {
        private readonly IAquisicaoService _aquisicaoService;

        public AquisicaosController(IAquisicaoService aquisicaoService)
        {
            _aquisicaoService = aquisicaoService;
        }

        // GET: api/Aquisicaos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aquisicao>>> GetAquisicaos()
        {
            var aquisicao = await _aquisicaoService.ObterTodosAsync();
            return Ok(aquisicao);
        }

        // GET: api/Aquisicaos/5 -- obter login por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Aquisicao>> GetAquisicao(int id)
        {
            var aquisicao = await _aquisicaoService.ObterPorIdAsync(id);

            if (aquisicao == null)
            {
                return NotFound();
            }

            return Ok(aquisicao);
        }

        // PUT: api/Aquisicaos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAquisicao(int id, [FromBody] AquisicaoDTO aquisicaoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _aquisicaoService.AtualizarAsync(id, aquisicaoDto);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }
        // POST: api/Aquisicaos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostAquisicao([FromBody] AquisicaoDTO aquisicaoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var aquisicao = new Aquisicao
            {
                Fornecedor = aquisicaoDto.Fornecedor,
                Valorpago = aquisicaoDto.Valorpago,
                Dataaquisicao = aquisicaoDto.Dataaquisicao,
                Origemveiculo = aquisicaoDto.Origemveiculo
            };

            await _aquisicaoService.CriarAsync(aquisicaoDto);
            return CreatedAtAction(nameof(GetAquisicao), new { id = aquisicao.IdAquisicao }, aquisicao);
        }


        // DELETE: api/Aquisicaos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAquisicao(int id)
        {
            var deleted = await _aquisicaoService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new {mensagem = deleted.Mensagem});
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }


        private async Task<ResultadoOperacao> AquisicaoExists(int id)
        {
            var aquisicao = await _aquisicaoService.ObterPorIdAsync(id);
            return aquisicao != null
                ? ResultadoOperacao.Ok("Aquisição encontrada.")
        : ResultadoOperacao.Falha("Aquisição não encontrada.");
        }
    }
}
