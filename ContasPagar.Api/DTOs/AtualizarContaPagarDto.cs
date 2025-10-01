using System;

namespace ContasPagar.Api.DTOs
{
    public class AtualizarContaPagarDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorOriginal { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}