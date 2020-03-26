using ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ConciliadorFinanceiro.Repository
{
    public class SqlDatabase<T> : IDatabase<T>
    {
        private readonly SqlConnection _scnConexao;

        public SqlDatabase()
        {
            _scnConexao = new SqlConnection
            {
                //TODO: ver como pegar a string de conexão da API
                ConnectionString = ""
            };

            Conectar();
        }

        public bool Conectar()
        {
            try
            {
                if (_scnConexao.State == System.Data.ConnectionState.Closed)
                    _scnConexao.Open();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Desconectar()
        {
            try
            {
                if (_scnConexao.State == System.Data.ConnectionState.Open)
                    _scnConexao.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<int> Cadastrar(T model)
        {
            throw new NotImplementedException();
        }

        public Task<int> Editar(T model)
        {
            throw new NotImplementedException();
        }

        public Task<int> Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Consultar(T model)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ConsultarLista(T model)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ConsultarLista()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Desconectar();
        }

    }
}
