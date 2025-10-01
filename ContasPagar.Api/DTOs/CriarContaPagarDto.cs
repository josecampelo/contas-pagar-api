using System;
using System.ComponentModel.DataAnnotations;

namespace ContasPagar.Api.DTOs
{
    /// <summary>
    /// Objeto utilizado para criar uma nova conta a pagar.
    /// </summary>
    public class CriarContaPagarDto
    {
        /// <summary>
        /// Nome ou descrição da conta.
        /// </summary>
        /// <example>Conta de Luz - Eletropaulo</example>
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(200, ErrorMessage = "O Nome deve ter no máximo 200 caracteres.")]
        public string Nome { get; set; }

        /// <summary>
        /// Valor original da conta, sem juros ou multas.
        /// </summary>
        /// <example>150.75</example>
        [Required(ErrorMessage = "O campo Valor Original é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O Valor Original deve ser maior que zero.")]
        public decimal ValorOriginal { get; set; }

        /// <summary>
        /// Data de vencimento original da conta.
        /// </summary>
        /// <example>2025-10-20</example>
        [Required(ErrorMessage = "O campo Data de Vencimento é obrigatório.")]
        public DateTime DataVencimento { get; set; }

        /// <summary>
        /// Data em que a conta foi ou será paga.
        /// </summary>
        /// <example>2025-10-18</example>
        [Required(ErrorMessage = "O campo Data de Pagamento é obrigatório.")]
        public DateTime DataPagamento { get; set; }
    }
}