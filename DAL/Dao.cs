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

        public virtual void Insere(T item)
        {
            var connection = DataBase.AbreConexao();

            ValidaCondicaoDeInsercao(item.RecebePropriedadeDeValidacao());

            var command = new FbCommand($"insert into {_nomeTabela} {item.RecebeNomeDasColunasDaTabelaParaSql()} values {item.RecebeValorDasPropriedadesParaSql()}", connection);

            var resultado = command.ExecuteNonQuery();

            connection.Close();

            if (resultado == 0)
            {
                throw new FalhaEmInserirException();
            }
        }

        public virtual void Atualiza(T item, bool mudouPropriedadeDeValidacao)
        {
            var connection = DataBase.AbreConexao();

            ValidaCondicaoAtualizacao(item.RecebePropriedadeDeValidacao(), mudouPropriedadeDeValidacao);

            var command = new FbCommand($"update {_nomeTabela} set {item.RecebeColunasIgualValorParaSql()} where id = {item.Id}", connection);

            var resultado = command.ExecuteNonQuery();

            connection.Close();

            if (resultado == 0)
            {
                throw new FalhaEmAtualizarException();
            }
        }

        public void Deleta(T item)
        {
            try
            {
                PesquisaPorId(item.Id);
            }
            catch (PesquisaSemSucessoException ex)
            {
                throw new FalhaEmDeletarException("Erro em deletar : item inexistente!", ex);
            }

            var connection = DataBase.AbreConexao();

            var command = new FbCommand($"delete from {_nomeTabela} where id = {item.Id}", connection);

            var resultado = command.ExecuteNonQuery();

            connection.Close();

            if (resultado == 0)
            {
                connection.Close();

                throw new FalhaEmDeletarException("Erro em deletar : erro no sql!");
            }
        }

        public IEnumerable<T> PesquisaTodos()
        {
            var connection = DataBase.AbreConexao();

            var command = new FbCommand($"select * from {_nomeTabela}", connection);

            return Pesquisa(command, connection);
        }

        public T PesquisaPorId(int id)
        {
            var connection = DataBase.AbreConexao();

            var command = new FbCommand($"select * from {_nomeTabela} where id = {id}", connection);

            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var values = CriaArrayDePropriedades(reader);

                connection.Close();

                return (T)new T().RecebeContaComPropriedadesDeCampos(values);
            }
            else
            {
                connection.Close();

                throw new PesquisaSemSucessoException();
            }
        }

        protected IEnumerable<T> Pesquisa(FbCommand command, FbConnection connection)
        {
            var reader = command.ExecuteReader();

            List<object[]> objects = new List<object[]>();

            while (reader.Read())
            {
                var values = CriaArrayDePropriedades(reader);
                objects.Add(values);
            }

            connection.Close();

            return CriaListaDePropriedades(objects);
        }

        protected IEnumerable<T> CriaListaDePropriedades(List<object[]> objects)
        {
            List<T> contas = new List<T>();

            objects
                .ForEach(x => contas.Add((T)new T().RecebeContaComPropriedadesDeCampos(x)));

            return contas;
        }

        protected object[] CriaArrayDePropriedades(FbDataReader reader)
        {
            var values = new object[reader.FieldCount];
            reader.GetValues(values);

            return values;
        }

        protected abstract void ValidaCondicaoDeInsercao(string validacao);
        protected abstract void ValidaCondicaoAtualizacao(string validacao, bool mudouPropriedadeDeValidacao);
    }
}
