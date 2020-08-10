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
    abstract class Dao<T> : IDao<T> where T : BaseModel
    {
        protected readonly string _nomeTabela;

        protected Dao(string nomeTabela)
        {
            _nomeTabela = nomeTabela;
        }

        public virtual void Insere(T item)
        {
            using var connection = DataBase.AbreConexao();

            ValidaCondicao(item);

            using var command = new FbCommand();
            command.Connection = connection;
            var commandtext = $"insert into {_nomeTabela} {item.RecebeNomeDasColunasDaTabelaParaSql()} values {item.RecebeValorDasPropriedadesParaSql()}";
            command.CommandText = commandtext;

            command.ExecuteNonQuery();
        }

        public virtual void Atualiza(T item)
        {
            using var connection = DataBase.AbreConexao();

            ValidaCondicao(item);

            using var command = new FbCommand($"update {_nomeTabela} set {item.RecebeColunasIgualValorParaSql()} where id = {item.Id}", connection);

            command.ExecuteNonQuery();
        }

        public void Deleta(T item)
        {
            try
            {
                PesquisaPorId(item.Id);
            }
            catch (PesquisaSemSucessoException)
            {
                throw new FalhaEmDeletarException("Item inexistente, portanto impossível deletar!");
            }

            using var connection = DataBase.AbreConexao();

            using var command = new FbCommand($"delete from {_nomeTabela} where id = {item.Id}", connection);

            command.ExecuteNonQuery();
        }

        public List<T> PesquisaTodos()
        {
            using var connection = DataBase.AbreConexao();

            using var command = new FbCommand($"select * from {_nomeTabela} order by id asc", connection);

            var reader = command.ExecuteReader();

            List<T> itens = new List<T>();

            while (reader.Read())
            {
                var propriedades = GeraPropriedadesParaConstrutor(reader);

                itens.Add(InstanciaObjeto(propriedades));
            }

            return itens;
        }

        public T PesquisaPorId(int id)
        {
            using var connection = DataBase.AbreConexao();

            using var command = new FbCommand($"select * from {_nomeTabela} where id = {id}", connection);

            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var propriedades = GeraPropriedadesParaConstrutor(reader);

                return (T)Activator.CreateInstance(typeof(T), propriedades);
            }
            else
            {
                throw new PesquisaSemSucessoException(_nomeTabela);
            }
        }

        protected T InstanciaObjeto(List<object> propriedades)
        {
            return (T)Activator.CreateInstance(typeof(T), propriedades);
        }

        protected List<object> GeraPropriedadesParaConstrutor(FbDataReader reader)
        {
            var values = new object[reader.FieldCount];
            reader.GetValues(values);

            return values.ToList();
        }
        protected void ValidaCondicao(T item)
        {
            try
            {
                var condicao = item.RecebePropriedadeDeValidacao();

                VerificaCondicao(item);
            }
            catch (PesquisaSemSucessoException)
            {
            }
        }

        protected abstract void VerificaCondicao(T item);
    }
}
