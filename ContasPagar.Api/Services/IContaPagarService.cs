using ContasPagar.Api.DTOs;
using System.Collections.Generic;

namespace ContasPagar.Api.Services
{
    /// <summary>
    /// Define o contrato para os serviços de gerenciamento de Contas a Pagar.
    /// </summary>
    public interface IContaPagarService
    {
        /// <summary>
        /// Cria uma nova conta a pagar com base nos dados fornecidos.
        /// </summary>
        /// <param name="contaPagar">Objeto DTO contendo os dados para a criação da nova conta.</param>
        /// <returns>O DTO da conta recém-criada, incluindo o ID gerado e os valores calculados.</returns>
        ContaPagarDto CriarContaPagar(CriarContaPagarDto contaPagar);

        /// <summary>
        /// Busca e retorna uma conta a pagar específica pelo seu ID.
        /// </summary>
        /// <param name="id">O ID da conta a pagar a ser pesquisada.</param>
        /// <returns>O DTO da conta encontrada ou nulo se nenhuma conta for encontrada com o ID fornecido.</returns>
        ContaPagarDto ObterContaPagarPorId(int id);

        /// <summary>
        /// Lista todas as contas a pagar cadastradas no sistema.
        /// </summary>
        /// <returns>Uma coleção (`IEnumerable`) de DTOs representando todas as contas a pagar.</returns>
        IEnumerable<ContaPagarDto> ListarContasPagar();

        /// <summary>
        /// Atualiza os dados de uma conta a pagar existente.
        /// </summary>
        /// <param name="contaPagar">Objeto DTO com os novos dados para a conta.</param>
        /// <returns>O DTO da conta atualizada ou nulo se a conta não for encontrada.</returns>
        ContaPagarDto AtualizarContaPagar(AtualizarContaPagarDto contaPagar);

        /// <summary>
        /// Exclui uma conta a pagar do sistema com base no seu ID.
        /// </summary>
        /// <param name="id">O ID da conta a pagar a ser excluída.</param>
        /// <returns>O DTO da conta que foi excluída ou nulo se a conta não for encontrada.</returns>
        ContaPagarDto ExcluirContaPagar(int id);
    }
}