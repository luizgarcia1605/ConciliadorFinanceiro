using ConciliadorFinanceiro.Base.Domain.Entities;
using ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesBusiness;
using ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConciliadorFinanceiro.Business
{
    public class LancamentoFinanceiroBusiness : ILancamentoFinanceiroBusiness
    {
        private readonly ILancamentoFinanceiroRepository _repositorioLancamento;

        public LancamentoFinanceiroBusiness(ILancamentoFinanceiroRepository repositorioLancamento)
        {
            _repositorioLancamento = repositorioLancamento;
        }

        public async Task<int> Cadastrar(LancamentoFinanceiro model)
        {
            return await _repositorioLancamento.Cadastrar(model);
        }

        public async Task<int> Editar(LancamentoFinanceiro model)
        {
            return await _repositorioLancamento.Editar(model);
        }

        public async Task<int> Deletar(int id)
        {
            return await _repositorioLancamento.Deletar(id);
        }

        public async Task<LancamentoFinanceiro> Consultar(int id)
        {
            return await _repositorioLancamento.Consultar(new LancamentoFinanceiro() { Id = id });
        }

        public async Task<LancamentoFinanceiro> Consultar(LancamentoFinanceiro model)
        {
            return await _repositorioLancamento.Consultar(model);
        }

        public async Task<List<LancamentoFinanceiro>> ConsultarLista(LancamentoFinanceiro model)
        {
            return await _repositorioLancamento.ConsultarLista(model);
        }

        public async Task<List<LancamentoFinanceiro>> ConsultarLista()
        {
            return await _repositorioLancamento.ConsultarLista();
        }

    }
}
