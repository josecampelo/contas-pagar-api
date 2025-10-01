using ContasPagar.Api.DTOs;
using ContasPagar.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ContasPagar.Api.Controllers
{
    [ApiController]
    [Route("v1/contas")]
    public class ContasPagarController : ControllerBase
    {
        private readonly IContaPagarService _contaPagarService;

        public ContasPagarController(IContaPagarService contaPagarService)
        {
            _contaPagarService = contaPagarService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContaPagarDto>> ListarTodas()
        {
            var contas = _contaPagarService.ListarContasPagar();
            return Ok(contas);
        }

        [HttpGet("{id}")]
        public ActionResult<ContaPagarDto> ObterPorId(int id)
        {
            var conta = _contaPagarService.ObterContaPagarPorId(id);

            if (conta == null)
            {
                return NotFound();
            }

            return Ok(conta);
        }

        [HttpPost]
        public ActionResult<ContaPagarDto> Criar([FromBody] CriarContaPagarDto criarDto)
        {
            var novaConta = _contaPagarService.CriarContaPagar(criarDto);

            return CreatedAtAction(nameof(ObterPorId), new { id = novaConta.Id }, novaConta);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] AtualizarContaPagarDto atualizarDto)
        {
            if (id != atualizarDto.Id)
            {
                return BadRequest();
            }

            var contaAtualizada = _contaPagarService.AtualizarContaPagar(atualizarDto);

            if (contaAtualizada == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            var contaExcluida = _contaPagarService.ExcluirContaPagar(id);

            if (contaExcluida == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}