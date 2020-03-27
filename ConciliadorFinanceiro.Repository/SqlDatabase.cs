using ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ConciliadorFinanceiro.Repository
{
    public class SqlDatabase : IDatabase
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

        public Task<bool> Conectar()
        {
            try
            {
                if (_scnConexao.State == System.Data.ConnectionState.Closed)
                    _scnConexao.Open();

                return new Task<bool>(() => true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> Desconectar()
        {
            try
            {
                if (_scnConexao.State == System.Data.ConnectionState.Open)
                    _scnConexao.Close();

                return new Task<bool>(() => true);
            }
            catch
            {
                return new Task<bool>(() => false);
            }
        }

        public Task<int> Cadastrar<T>(T model)
        {
            throw new NotImplementedException();
        }

        public Task<int> Editar<T>(T model)
        {
            throw new NotImplementedException();
        }

        public Task<int> Deletar<T>(T model)
        {
            throw new NotImplementedException();
        }

        public Task<T> Consultar<T>(T model)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ConsultarLista<T>(T model)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ConsultarLista<T>()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Desconectar();
        }

    }
}
