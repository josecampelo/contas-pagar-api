using System;

namespace ContasPagar.Api.DTOs
{
    /// <summary>
    /// Representa os dados de uma conta a pagar que são retornados pela API.
    /// </summary>
    public class ContaPagarDto
    {
        /// <summary>
        /// ID único da conta, gerado pelo sistema.
        /// </summary>
        /// <example>101</example>
        public int Id { get; set; }

        /// <summary>
        /// Nome ou descrição da conta.
        /// </summary>
        /// <example>Fatura Cartão de Crédito - Nubank</example>
        public string Nome { get; set; }

        /// <summary>
        /// Valor original da conta, sem juros ou multas.
        /// </summary>
        /// <example>1200.00</example>
        public decimal ValorOriginal { get; set; }

        /// <summary>
        /// Valor final da conta após a aplicação de juros e multas por atraso.
        /// </summary>
        /// <example>1226.40</example>
        public decimal ValorCorrigido { get; set; }

        /// <summary>
        /// Número de dias em atraso, calculado com base nas datas de vencimento e pagamento.
        /// </summary>
        /// <example>2</example>
        public int QuantidadeDiasAtraso { get; set; }

        /// <summary>
        /// Data em que a conta foi efetivamente paga.
        /// </summary>
        /// <example>2025-09-22</example>
        public DateTime DataPagamento { get; set; }
    }
}