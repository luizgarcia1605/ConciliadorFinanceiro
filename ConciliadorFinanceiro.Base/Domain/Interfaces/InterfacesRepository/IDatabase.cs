using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesRepository
{
    public interface IDatabase
    {
        Task<bool> Conectar();
        Task<bool> Desconectar();
        Task<int> Cadastrar<T>(T model);
        Task<int> Editar<T>(T model);
        Task<int> Deletar<T>(T model);
        Task<T> Consultar<T>(T model);
        Task<List<T>> ConsultarLista<T>(T model);
        Task<List<T>> ConsultarLista<T>();
        Task<List<T>> ConsultarLista<T>(List<string> condicoes);
    }
}
