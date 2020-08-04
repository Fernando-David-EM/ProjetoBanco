using Banco.Exceptions;
using Banco.Model;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Isql;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Util
{
    class CommandHelper<T> where T : BaseModel, new()
    {
        private FbConnection _connection;
        private FbCommand _command;
        private FbTransaction _transaction;
        private string _tabela;

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
            _command = new FbCommand($"select * from {_tabela} where id = {id}");

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
            _command = new FbCommand($"delete from {_tabela} where id = {id}");

            var resultado = _command.ExecuteNonQuery();

            if (resultado == 0)
            {
                throw new FalhaEmDeletarException();
            }
        }

        public void ExecuteInsert(T item)
        {
            _command = new FbCommand($"insert into {_tabela} {item.GetNameOfTableColumns()} values {item.GetValueOfTableProperties()}");

            var resultado = _command.ExecuteNonQuery();

            if (resultado == 0)
            {
                throw new FalhaEmInserirException();
            }
        }

        public void ExecuteUpdate(T item)
        {
            _command = new FbCommand($"update {_tabela} set {item.GetColumnEqualsValue()} where id = id");

            var resultado = _command.ExecuteNonQuery();

            if (resultado == 0)
            {
                throw new FalhaEmAtualizarException();
            }
        }

        private IEnumerable<T> Search()
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

        private IEnumerable<T> CreateList(List<object[]> objects)
        {
            List<T> contas = new List<T>();

            objects
                .ForEach(x => contas.Add((T)new T().SetPropertiesFromObjectArray(x)));

            return contas;
        }

        private object[] CreateArrayOfValues(FbDataReader reader)
        {
            var values = new object[reader.FieldCount];
            reader.GetValues(values);

            return values;
        }
    }
}
