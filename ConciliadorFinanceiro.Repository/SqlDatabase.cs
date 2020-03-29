using ConciliadorFinanceiro.Base.Domain.Enums;
using ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesRepository;
using ConciliadorFinanceiro.Util.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

        public async Task<int> Cadastrar<T>(T model)
        {
            try
            {
                var tabela = typeof(T).Name;
                var mapa = Mapper.MapearClasseDB(model, out string campoId);

                mapa.Remove(campoId);

                var campos = String.Join(',', mapa.Keys.ToList());
                var valores = String.Join(',', mapa.Values.ToList());

                var comando = $@"INSERT INTO {tabela} ({campos}) VALUES ({valores})";
                var scmComando = new SqlCommand(comando, _scnConexao);
                scmComando.ExecuteNonQuery();

                comando = "SELECT @@Identity";
                scmComando = new SqlCommand(comando, _scnConexao);

                return await Task.FromResult(int.Parse(scmComando.ExecuteScalar().ToString()));
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Editar<T>(T model)
        {
            try
            {
                var tabela = typeof(T).Name;
                var mapa = Mapper.MapearClasseDB(model, out string campoId);

                var id = mapa.Where(k => k.Key == campoId).FirstOrDefault();
                var where = $"{id.Key} = {id.Value}";

                mapa.Remove(campoId);

                var campos = new List<string>();
                mapa.ToList().ForEach(c => campos.Add(c.Key + " = " + c.Value));
                var camposvalores = String.Join(',', campos);

                var comando = $@"UPDATE {tabela} SET {camposvalores} WHERE {where}";
                var scmComando = new SqlCommand(comando, _scnConexao);
                scmComando.ExecuteNonQuery();

                return await Task.FromResult(Convert.ToInt32(id.Value));
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Deletar<T>(T model)
        {
            var tabela = typeof(T).Name;
            var mapa = Mapper.MapearClasseDB(model, out string campoId);

            var id = mapa.Where(k => k.Key == campoId).FirstOrDefault();
            var where = $"{id.Key} = {id.Value}";

            var comando = $@"DELETE FROM {tabela} WHERE {where}";
            var scmComando = new SqlCommand(comando, _scnConexao);
            scmComando.ExecuteNonQuery();

            return await Task.FromResult(Convert.ToInt32(id.Value));
        }

        public async Task<T> Consultar<T>(T model)
        {
            return await Task.FromResult(Consultar(model, true).FirstOrDefault());
        }

        public async Task<List<T>> ConsultarLista<T>(T model)
        {
            return await Task.FromResult(Consultar(model, true));
        }

        public async Task<List<T>> ConsultarLista<T>()
        {
            return await Task.FromResult(Consultar<T>());
        }

        public async Task<List<T>> ConsultarLista<T>(List<string> condicoes)
        {
            return await Task.FromResult(Consultar<T>(condicoes));
        }

        private List<T> Consultar<T>(T model, bool filtrar)
        {
            var datTabela = new DataTable();
            var tabela = typeof(T).Name;
            var where = string.Empty;
            var mapa = Mapper.MapearClasseDB(model, out string campoId);

            var id = mapa.Where(k => k.Key == campoId).FirstOrDefault();

            if (filtrar)
                where = $"WHERE {id.Key} = {id.Value}";

            var campos = String.Join(',', mapa.Keys.ToList());

            var comando = $@"SELECT {campos} FROM {tabela} {where}";
            var scmComando = new SqlCommand(comando, _scnConexao);
            var sdaAdaptador = new SqlDataAdapter(scmComando);
            sdaAdaptador.Fill(datTabela);

            var json = JsonConvert.SerializeObject(datTabela);
            var retorno = JsonConvert.DeserializeObject<List<T>>(json);

            return retorno;
        }

        private List<T> Consultar<T>(List<string> condicoes = null)
        {
            var datTabela = new DataTable();
            var tabela = typeof(T).Name;
            var where = string.Empty;

            if (condicoes != null && condicoes.Count != 0)
                where = $"WHERE {String.Join(" AND ", condicoes)}";

            var comando = $@"SELECT * FROM {tabela} {where}";

            var scmComando = new SqlCommand(comando, _scnConexao);
            var sdaAdaptador = new SqlDataAdapter(scmComando);
            sdaAdaptador.Fill(datTabela);

            var json = JsonConvert.SerializeObject(datTabela);
            var retorno = JsonConvert.DeserializeObject<List<T>>(json);

            return retorno;
        }

        public override void Dispose()
        {
            Desconectar();
        }

    }
}
