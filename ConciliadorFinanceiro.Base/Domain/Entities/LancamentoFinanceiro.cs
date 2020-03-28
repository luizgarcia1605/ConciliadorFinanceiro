﻿using System;
using ConciliadorFinanceiro.Base.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ConciliadorFinanceiro.Base.Domain.Entities
{
    public class LancamentoFinanceiro
    {
        [Key]
        public int Id { get; set; }

        public DateTime DataHoraLancamento { get; set; }

        [RegularExpression(@"^(?<num>\d*)(?<dot>.[0-9]*)?$", ErrorMessage = "Valor informado é inválido")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }

        [RegularExpression(@"^[1-2]$", ErrorMessage = "Permitidos valores apenas entre 1 (Debito) e 2 (Credito)")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EnumDataType(typeof(TipoLancamento))]
        public int Tipo { get; set; }

        [RegularExpression(@"^[1-2]$", ErrorMessage = "Permitidos valores apenas entre 1 (NaoConciliado) e 2 (Conciliado)")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EnumDataType(typeof(StatusLancamento))]
        public int Status { get; set; }
    }
}