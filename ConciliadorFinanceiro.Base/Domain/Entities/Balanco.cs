using System;
using System.ComponentModel.DataAnnotations;

namespace ConciliadorFinanceiro.Base.Domain.Entities
{
    public class Balanco
    {
        public DateTime DataBalanco { get; set; }

        [DataType(DataType.Currency)]
        public decimal ValorTotalCredito { get; set; }

        [DataType(DataType.Currency)]
        public decimal ValorTotalDebito { get; set; }

        [DataType(DataType.Currency)]
        public decimal ValorSaldo { get; set; }

        [DataType(DataType.Currency)]
        public decimal FluxoCaixa { get; set; }

        public bool Somatoria { get; set; }
    }
}