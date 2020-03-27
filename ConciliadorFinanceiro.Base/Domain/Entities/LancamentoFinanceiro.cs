using System;
using ConciliadorFinanceiro.Base.Domain.Enums;

namespace ConciliadorFinanceiro.Base.Domain.Entities
{
    public class LancamentoFinanceiro
    {
        public int Id { get; set; }
        public DateTime DataHoraLancamento { get; set; }
        public decimal Valor { get; set; }
        public TipoLancamento Tipo { get; set; }
        public StatusLancamento Status { get; set; }
    }
}