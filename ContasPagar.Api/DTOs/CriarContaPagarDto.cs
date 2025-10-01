using System;
using System.ComponentModel.DataAnnotations;

namespace ContasPagar.Api.DTOs
{
    public class CriarContaPagarDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(200, ErrorMessage = "O Nome deve ter no máximo 200 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Valor Original é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O Valor Original deve ser maior que zero.")]
        public decimal ValorOriginal { get; set; }

        [Required(ErrorMessage = "O campo Data de Vencimento é obrigatório.")]
        public DateTime DataVencimento { get; set; }

        [Required(ErrorMessage = "O campo Data de Pagamento é obrigatório.")]
        public DateTime DataPagamento { get; set; }
    }
}