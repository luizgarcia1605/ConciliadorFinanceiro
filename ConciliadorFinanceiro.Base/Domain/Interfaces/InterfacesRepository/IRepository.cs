using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesRepository
{
    public interface IRepository<T> : IDisposable
    {
        Task<int> Cadastrar(T model);
        Task<int> Editar(T model);
        Task<int> Deletar(T model);
        Task<T> Consultar(T model);
        Task<List<T>> ConsultarLista(T model);
        Task<List<T>> ConsultarLista();
        Task<List<T>> ConsultarLista(List<string> condicoes);
    }
}