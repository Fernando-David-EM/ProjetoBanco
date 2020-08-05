﻿using Banco.Exceptions;
using Banco.Model;
using FirebirdSql.Data.FirebirdClient;
using System.Collections.Generic;

namespace Banco.Util
{
    class CommandHelper<T> where T : BaseModel, new()
    {
        protected FbConnection _connection;
        protected FbCommand _command;
        protected FbTransaction _transaction;
        protected string _tabela;

        public CommandHelper(string tabela, FbConnection connection, FbTransaction transaction)
        {
            _tabela = tabela;
            _connection = connection;
            _transaction = transaction;
        }

        public IEnumerable<T> ExecuteSearchAll()
        {
            _command = new FbCommand($"select * from {_tabela}", _connection, _transaction);

            return Search();
        }

        public T ExecuteSearchById(int id)
        {
            _command = new FbCommand($"select * from {_tabela} where id = {id}", _connection, _transaction);

            var reader = _command.ExecuteReader();

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

        public void ExecuteDelete(int id)
        {
            _command = new FbCommand($"delete from {_tabela} where id = {id}", _connection, _transaction);

            var resultado = _command.ExecuteNonQuery();

            if (resultado == 0)
            {
                throw new FalhaEmDeletarException();
            }
        }

        public virtual void ExecuteInsert(T item)
        {
            _command = new FbCommand($"insert into {_tabela} {item.GetNameOfTableColumns()} values {item.GetValueOfTableProperties()}", _connection, _transaction);

            var resultado = _command.ExecuteNonQuery();

            if (resultado == 0)
            {
                throw new FalhaEmInserirException();
            }
        }

        public virtual void ExecuteUpdate(T item)
        {
            _command = new FbCommand($"update {_tabela} set {item.GetColumnEqualsValue()} where id = id", _connection, _transaction);

            var resultado = _command.ExecuteNonQuery();

            if (resultado == 0)
            {
                throw new FalhaEmAtualizarException();
            }
        }

        protected IEnumerable<T> Search()
        {
            var reader = _command.ExecuteReader();

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
    }
}
