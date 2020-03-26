﻿using System;

namespace ConciliadorFinanceiro.Base.Domain.Entities
{
    public class BalancoDia
    {
        public DateTime DataBalanco { get; set; }
        public decimal ValorTotalCredito { get; set; }
        public decimal ValorTotalDebito { get; set; }
        public decimal ValorSaldo { get; set; }
    }
}
