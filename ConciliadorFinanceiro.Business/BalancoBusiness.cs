using ConciliadorFinanceiro.Base.Domain.Entities;
using ConciliadorFinanceiro.Base.Domain.Enums;
using ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesBusiness;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ConciliadorFinanceiro.Business
{
    public class BalancoBusiness : IBalancoBusiness
    {
        public async Task<List<Balanco>> GerarBalancoDiario(List<LancamentoFinanceiro> lancamentosFinanceiros)
        {
            lancamentosFinanceiros = lancamentosFinanceiros.OrderBy(l => l.DataHoraLancamento).ToList();

            var balancoDiario = new List<Balanco>();
            var saldoAnterior = 0M;

            foreach (var lancamento in lancamentosFinanceiros)
            {
                var balancoDia = new Balanco();

                if (lancamento.Tipo == (int)TipoLancamento.Debito)
                {
                    balancoDia.ValorTotalDebito = lancamento.Valor;
                    balancoDia.ValorSaldo -= lancamento.Valor;
                }
                else if (lancamento.Tipo == (int)TipoLancamento.Credito)
                {
                    balancoDia.ValorTotalCredito = lancamento.Valor;
                    balancoDia.ValorSaldo += lancamento.Valor;
                }

                saldoAnterior += balancoDia.ValorSaldo;
                balancoDia.ValorSaldo = saldoAnterior;

                balancoDia.DataBalanco = lancamento.DataHoraLancamento;

                balancoDiario.Add(balancoDia);
            }

            return await Task.FromResult(balancoDiario);
        }

        public async Task<List<Balanco>> GerarBalancoMensal(List<LancamentoFinanceiro> lancamentosFinanceiros)
        {
            var balancoDiario = await GerarBalancoDiario(lancamentosFinanceiros);

            balancoDiario = balancoDiario.GroupBy(b => b.DataBalanco.ToString("yyyy-MM-dd"))
                                            .Select(g => new Balanco()
                                            {
                                                DataBalanco = Convert.ToDateTime(g.First().DataBalanco.ToString("yyyy-MM-dd")),
                                                ValorTotalCredito = g.Sum(s => s.ValorTotalCredito),
                                                ValorTotalDebito = g.Sum(s => s.ValorTotalDebito),
                                                ValorSaldo = g.Sum(s => s.ValorTotalCredito) - g.Sum(s => s.ValorTotalDebito),
                                            }).ToList();

            return await Task.FromResult(balancoDiario);
        }
    }
}
