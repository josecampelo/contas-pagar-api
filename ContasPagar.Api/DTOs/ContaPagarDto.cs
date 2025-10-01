using System;

namespace ContasPagar.Api.DTOs
{
    public class ContaPagarDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorOriginal { get; set; }
        public decimal ValorCorrigido { get; set; }
        public int QuantidadeDiasAtraso { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}