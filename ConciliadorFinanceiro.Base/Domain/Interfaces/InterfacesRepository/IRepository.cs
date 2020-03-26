using ConciliadorFinanceiro.Base.Domain.Entities;
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
        Task<int> Deletar(int id);
        Task<T> Consultar(T model);
        Task<List<T>> ConsultarLista(T model);
        Task<List<T>> ConsultarLista();
    }
}
