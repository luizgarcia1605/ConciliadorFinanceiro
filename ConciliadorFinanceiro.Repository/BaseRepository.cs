using ConciliadorFinanceiro.Base.Domain.Entities;
using ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConciliadorFinanceiro.Repository
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        private readonly IDatabase _database;

        public BaseRepository(IDatabase database)
        {
            _database = database;
        }

        public virtual async Task<int> Cadastrar(T model)
        {
            return await _database.Cadastrar(model);
        }

        public virtual async Task<int> Editar(T model)
        {
            return await _database.Editar(model);
        }

        public virtual async Task<int> Deletar(T model)
        {
            return await _database.Deletar(model);
        }

        public virtual async Task<T> Consultar(T model)
        {
            return await _database.Consultar(model);
        }

        public virtual async Task<List<T>> ConsultarLista(T model)
        {
            return await _database.ConsultarLista(model);
        }

        public virtual async Task<List<T>> ConsultarLista()
        {
            return await _database.ConsultarLista<T>();
        }

        public virtual void Dispose()
        {
        }

    }
}
