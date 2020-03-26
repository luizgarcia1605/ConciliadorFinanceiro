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
        public virtual async Task<int> Cadastrar(T model)
        {
            using (var db = new SqlDatabase<T>())
                return await db.Cadastrar(model);
        }

        public virtual async Task<int> Editar(T model)
        {
            using (var db = new SqlDatabase<T>())
                return await db.Editar(model);
        }

        public virtual async Task<int> Deletar(int id)
        {
            using (var db = new SqlDatabase<T>())
                return await db.Deletar(id);
        }

        public virtual async Task<T> Consultar(T model)
        {
            using (var db = new SqlDatabase<T>())
                return await db.Consultar(model);
        }

        public virtual async Task<List<T>> ConsultarLista(T model)
        {
            using (var db = new SqlDatabase<T>())
                return await db.ConsultarLista(model);
        }

        public virtual async Task<List<T>> ConsultarLista()
        {
            using (var db = new SqlDatabase<T>())
                return await db.ConsultarLista();
        }

        public virtual void Dispose()
        {
            this.Dispose();
        }

    }
}
