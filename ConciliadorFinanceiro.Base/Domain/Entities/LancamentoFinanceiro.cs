using System;
using ConciliadorFinanceiro.Base.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ConciliadorFinanceiro.Base.Domain.Entities
{
    public class LancamentoFinanceiro
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime DataHoraLancamento { get; set; }
        
        public decimal Valor { get; set; }

        [EnumDataType(typeof(TipoLancamento))]
        public int Tipo { get; set; }

        [EnumDataType(typeof(StatusLancamento))]
        public int Status { get; set; }
    }
}