using ConciliadorFinanceiro.Base.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesBusiness
{
    public interface ILancamentoFinanceiroBusiness
    {
        Task<int> Cadastrar(LancamentoFinanceiro model);
        Task<int> Editar(LancamentoFinanceiro model);
        Task<int> Deletar(int id);
        Task<int> Deletar(LancamentoFinanceiro model);
        Task<LancamentoFinanceiro> Consultar(int id);
        Task<LancamentoFinanceiro> Consultar(LancamentoFinanceiro model);
        Task<List<LancamentoFinanceiro>> ConsultarLista(LancamentoFinanceiro model);
        Task<List<LancamentoFinanceiro>> ConsultarLista();
    }
}
