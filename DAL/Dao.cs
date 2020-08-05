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
        protected FbConnection _connection = null;
        protected FbTransaction _transaction = null;
        protected readonly string _nomeTabela;

        protected Dao(string nomeTabela)
        {
            _connection = DataBase.GetConexao();
            _transaction = DataBase.GetTransaction();
            _nomeTabela = nomeTabela;
        }

        public virtual void Insert(T item)
        {
            ValidaCondicaoInsert(item.GetPropriedadeDeValidacao());

            var command = new FbCommand($"insert into {_nomeTabela} {item.GetNameOfTableColumns()} values {item.GetValueOfTableProperties()}", _connection, _transaction);

            var resultado = command.ExecuteNonQuery();

            DataBase.CommitTransaction();

            if (resultado == 0)
            {
                throw new FalhaEmInserirException();
            }
        }

        public virtual void Update(T item, bool mudouPropriedadeDeValidacao)
        {
            ValidaCondicaoUpdate(item.GetPropriedadeDeValidacao(), mudouPropriedadeDeValidacao);

            var command = new FbCommand($"update {_nomeTabela} set {item.GetColumnEqualsValue()} where id = {item.Id}", _connection, _transaction);

            var resultado = command.ExecuteNonQuery();

            DataBase.CommitTransaction();

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

            var command = new FbCommand($"delete from {_nomeTabela} where id = {item.Id}", _connection, _transaction);

            var resultado = command.ExecuteNonQuery();

            DataBase.CommitTransaction();

            if (resultado == 0)
            {
                throw new FalhaEmDeletarException("Erro em deletar : erro no sql!");
            }
        }

        public IEnumerable<T> GetAll()
        {
            var command = new FbCommand($"select * from {_nomeTabela}", _connection, _transaction);

            return Search(command);
        }

        public T GetByID(int id)
        {
            var command = new FbCommand($"select * from {_nomeTabela} where id = {id}", _connection, _transaction);

            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var values = CreateArrayOfValues(reader);
                return (T)new T().SetPropertiesFromObjectArray(values);
            }
            else
            {
                throw new PesquisaSemSucessoException();
            }
        }

        protected IEnumerable<T> Search(FbCommand command)
        {
            var reader = command.ExecuteReader();

            List<object[]> objects = new List<object[]>();

            while (reader.Read())
            {
                var values = CreateArrayOfValues(reader);
                objects.Add(values);
            }

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
