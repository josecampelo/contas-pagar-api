using System;
using System.ComponentModel.DataAnnotations;

namespace ContasPagar.Api.DTOs
{
    /// <summary>
    /// Objeto utilizado para atualizar os dados de uma conta a pagar existente.
    /// </summary>
    public class AtualizarContaPagarDto
    {
        /// <summary>
        /// ID da conta a pagar que será atualizada.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Novo nome ou descrição da conta.
        /// </summary>
        /// <example>Internet - Vivo Fibra</example>
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(200, ErrorMessage = "O Nome deve ter no máximo 200 caracteres.")]
        public string Nome { get; set; }

        /// <summary>
        /// Novo valor original da conta.
        /// </summary>
        /// <example>125.00</example>
        [Required(ErrorMessage = "O campo Valor Original é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O Valor Original deve ser maior que zero.")]
        public decimal ValorOriginal { get; set; }

        /// <summary>
        /// Nova data de vencimento da conta.
        /// </summary>
        /// <example>2025-11-15</example>
        [Required(ErrorMessage = "O campo Data de Vencimento é obrigatório.")]
        public DateTime DataVencimento { get; set; }
    }
}