using ContasPagar.Api.DTOs;
using ContasPagar.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ContasPagar.Api.Controllers
{
    /// <summary>
    /// Endpoints para gerenciar Contas a Pagar.
    /// </summary>
    [ApiController]
    [Route("v1/contas")]
    public class ContasPagarController : ControllerBase
    {
        private readonly IContaPagarService _contaPagarService;

        public ContasPagarController(IContaPagarService contaPagarService)
        {
            _contaPagarService = contaPagarService;
        }

        /// <summary>
        /// Lista todas as contas a pagar cadastradas.
        /// </summary>
        /// <returns>Uma lista de contas a pagar.</returns>
        /// <response code="200">Retorna a lista de contas com sucesso.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ContaPagarDto>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ContaPagarDto>> ListarTodas()
        {
            var contas = _contaPagarService.ListarContasPagar();
            return Ok(contas);
        }

        /// <summary>
        /// Busca uma conta a pagar específica pelo seu ID.
        /// </summary>
        /// <param name="id">O ID da conta a pagar a ser pesquisada.</param>
        /// <returns>Os dados da conta a pagar encontrada.</returns>
        /// <response code="200">Retorna os dados da conta encontrada.</response>
        /// <response code="404">Se nenhuma conta com o ID especificado for encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ContaPagarDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ContaPagarDto> ObterPorId(int id)
        {
            var conta = _contaPagarService.ObterContaPagarPorId(id);

            if (conta == null)
            {
                return NotFound();
            }

            return Ok(conta);
        }

        /// <summary>
        /// Cadastra uma nova conta a pagar no sistema.
        /// </summary>
        /// <param name="criarDto">Objeto com os dados necessários para a criação da conta.</param>
        /// <returns>A conta recém-criada.</returns>
        /// <response code="201">Retorna a conta recém-criada.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ContaPagarDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ContaPagarDto> Criar([FromBody] CriarContaPagarDto criarDto)
        {
            var novaConta = _contaPagarService.CriarContaPagar(criarDto);

            return CreatedAtAction(nameof(ObterPorId), new { id = novaConta.Id }, novaConta);
        }

        /// <summary>
        /// Atualiza os dados de uma conta a pagar existente.
        /// </summary>
        /// <param name="id">O ID da conta a pagar a ser atualizada.</param>
        /// <param name="atualizarDto">Objeto com os novos dados para a conta.</param>
        /// <response code="204">Indica que a conta foi atualizada com sucesso.</response>
        /// <response code="400">Se o ID da URL não corresponder ao ID do corpo da requisição.</response>
        /// <response code="404">Se nenhuma conta com o ID especificado for encontrada.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Exclui uma conta a pagar do sistema.
        /// </summary>
        /// <param name="id">O ID da conta a pagar a ser excluída.</param>
        /// <response code="204">Indica que a conta foi excluída com sucesso.</response>
        /// <response code="404">Se nenhuma conta com o ID especificado for encontrada.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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