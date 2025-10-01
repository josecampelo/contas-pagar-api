using ContasPagar.Api.DTOs;
using System.Collections.Generic;

namespace ContasPagar.Api.Services
{
    public interface IContaPagarService
    {
        ContaPagarDto CriarContaPagar(CriarContaPagarDto contaPagar);
        ContaPagarDto ObterContaPagarPorId(int id);
        IEnumerable<ContaPagarDto> ListarContasPagar();
        ContaPagarDto AtualizarContaPagar(AtualizarContaPagarDto contaPagar);
        ContaPagarDto ExcluirContaPagar(int id);
    }
}