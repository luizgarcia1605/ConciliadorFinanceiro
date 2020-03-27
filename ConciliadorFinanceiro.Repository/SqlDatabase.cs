using ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ConciliadorFinanceiro.Repository
{
    public class SqlDatabase : DbContext, IDatabase
    {
        private readonly SqlConnection _scnConexao;

        public SqlDatabase(DbContextOptions<SqlDatabase> options) : base(options)
        {
            _scnConexao = new SqlConnection
            {
                ConnectionString = options.FindExtension<SqlServerOptionsExtension>().ConnectionString
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

        public override void Dispose()
        {
            Desconectar();
        }

    }
}
