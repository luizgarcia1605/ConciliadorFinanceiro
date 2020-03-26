using System;
using System.Collections.Generic;
using System.Text;

namespace ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesRepository
{
    public interface IDatabase<T> : IRepository<T>
    {
        bool Conectar();
        bool Desconectar();
    }
}
