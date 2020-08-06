using Banco.Data;
using Banco.Exceptions;
using Banco.Model;
using Banco.Util;
using FirebirdSql.Data.FirebirdClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.DAL
{
    abstract class Dao<T> : IDao<T> where T : BaseModel, new()
    {
        protected readonly string _nomeTabela;

        protected Dao(string nomeTabela)
        {
            _nomeTabela = nomeTabela;
        }

        public virtual void Insert(T item)
        {
            var connection = new DataBase().AbrirConexao();

            ValidaCondicaoInsert(item.GetPropriedadeDeValidacao());

            var command = new FbCommand($"insert into {_nomeTabela} {item.GetNameOfTableColumns()} values {item.GetValueOfTableProperties()}", connection);

            var resultado = command.ExecuteNonQuery();

            connection.Close();

            if (resultado == 0)
            {
                throw new FalhaEmInserirException();
            }
        }

        public virtual void Update(T item, bool mudouPropriedadeDeValidacao)
        {
            var connection = new DataBase().AbrirConexao();

            ValidaCondicaoUpdate(item.GetPropriedadeDeValidacao(), mudouPropriedadeDeValidacao);

            var command = new FbCommand($"update {_nomeTabela} set {item.GetColumnEqualsValue()} where id = {item.Id}", connection);

            var resultado = command.ExecuteNonQuery();

            connection.Close();

            if (resultado == 0)
            {
                throw new FalhaEmAtualizarException();
            }
        }

        public void Delete(T item)
        {
            try
            {
                GetByID(item.Id);
            }
            catch (PesquisaSemSucessoException ex)
            {
                throw new FalhaEmDeletarException("Erro em deletar : item inexistente!", ex);
            }

            var connection = new DataBase().AbrirConexao();

            var command = new FbCommand($"delete from {_nomeTabela} where id = {item.Id}", connection);

            var resultado = command.ExecuteNonQuery();

            connection.Close();

            if (resultado == 0)
            {
                connection.Close();

                throw new FalhaEmDeletarException("Erro em deletar : erro no sql!");
            }
        }

        public IEnumerable<T> GetAll()
        {
            var connection = new DataBase().AbrirConexao();

            var command = new FbCommand($"select * from {_nomeTabela}", connection);

            return Search(command, connection);
        }

        public T GetByID(int id)
        {
            var connection = new DataBase().AbrirConexao();

            var command = new FbCommand($"select * from {_nomeTabela} where id = {id}", connection);

            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var values = CreateArrayOfValues(reader);

                connection.Close();

                return (T)new T().SetPropertiesFromObjectArray(values);
            }
            else
            {
                connection.Close();

                throw new PesquisaSemSucessoException();
            }
        }

        protected IEnumerable<T> Search(FbCommand command, FbConnection connection)
        {
            var reader = command.ExecuteReader();

            List<object[]> objects = new List<object[]>();

            while (reader.Read())
            {
                var values = CreateArrayOfValues(reader);
                objects.Add(values);
            }

            connection.Close();

            return CreateList(objects);
        }

        protected IEnumerable<T> CreateList(List<object[]> objects)
        {
            List<T> contas = new List<T>();

            objects
                .ForEach(x => contas.Add((T)new T().SetPropertiesFromObjectArray(x)));

            return contas;
        }

        protected object[] CreateArrayOfValues(FbDataReader reader)
        {
            var values = new object[reader.FieldCount];
            reader.GetValues(values);

            return values;
        }

        protected abstract void ValidaCondicaoInsert(string validacao);
        protected abstract void ValidaCondicaoUpdate(string validacao, bool mudouPropriedadeDeValidacao);
    }
}
