using System;

namespace ContasPagar.Api.DTOs
{
    public class CriarContaPagarDto
    {
        public string Nome { get; set; }
        public decimal ValorOriginal { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}