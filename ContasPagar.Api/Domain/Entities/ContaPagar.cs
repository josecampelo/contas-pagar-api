using System;

namespace ContasPagar.Api.Domain.Entities
{
    public class ContaPagar
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorOriginal { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorCorrigido { get; set; }
        public int QuantidadeDiasAtraso { get; set; }
        public decimal PercentualMultaAplicado { get; set; }
        public decimal PercentualJurosDiaAplicado { get; set; }
    }
}
