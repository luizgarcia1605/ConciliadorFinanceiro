using ConciliadorFinanceiro.Base.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesBusiness
{
    public interface IBalancoBusiness
    {
        Task<List<Balanco>> GerarBalancoDiario(List<LancamentoFinanceiro> lancamentosFinanceiros);
        Task<List<Balanco>> GerarBalancoPeriodo(List<LancamentoFinanceiro> lancamentosFinanceiros);
    }
}
