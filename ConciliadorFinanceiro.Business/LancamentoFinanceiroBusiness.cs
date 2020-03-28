using ConciliadorFinanceiro.Base.Domain.Entities;
using ConciliadorFinanceiro.Base.Domain.Enums;
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
            if (model == null)
                throw new ArgumentException("Objeto vazio");

            model.DataHoraLancamento = DateTime.Now;

            return await _repositorioLancamento.Cadastrar(model);
        }

        public async Task<int> Editar(LancamentoFinanceiro model)
        {
            var modelAtualizar = await Consultar(model);

            if (modelAtualizar == null || modelAtualizar.Status == (int)StatusLancamento.Conciliado)
                throw new ArgumentException("Objeto vazio ou já conciliado");

            modelAtualizar.Valor = model.Valor;
            modelAtualizar.Tipo = model.Tipo;
            modelAtualizar.Status = model.Status;

            return await _repositorioLancamento.Editar(modelAtualizar);
        }

        public async Task<int> Deletar(int id)
        {
            var modelDeletar = await Consultar(id);

            if (modelDeletar == null || modelDeletar.Status == (int)StatusLancamento.Conciliado)
                throw new ArgumentException("Objeto vazio ou já conciliado");

            return await _repositorioLancamento.Deletar(new LancamentoFinanceiro() { Id = id });
        }

        public async Task<int> Deletar(LancamentoFinanceiro model)
        {
            var modelDeletar = await Consultar(model);

            if (modelDeletar == null || modelDeletar.Status == (int)StatusLancamento.Conciliado)
                throw new ArgumentException("Objeto vazio ou já conciliado");

            return await _repositorioLancamento.Deletar(model);
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

        public async Task<List<LancamentoFinanceiro>> ConsultarLista(List<string> condicoes)
        {
            return await _repositorioLancamento.ConsultarLista(condicoes);
        }

    }
}
