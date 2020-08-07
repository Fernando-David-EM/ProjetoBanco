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

            ValidaCondicaoDeInsercao(item.RecebePropriedadeDeValidacao());

            using var command = new FbCommand($"insert into {_nomeTabela} {item.RecebeNomeDasColunasDaTabelaParaSql()} values {item.RecebeValorDasPropriedadesParaSql()}", connection);

            var resultado = command.ExecuteNonQuery();

            if (resultado == 0)
            {
                throw new FalhaEmInserirException();
            }
        }

        public virtual void Atualiza(T item, bool mudouPropriedadeDeValidacao)
        {
            using var connection = DataBase.AbreConexao();

            ValidaCondicaoAtualizacao(item.RecebePropriedadeDeValidacao(), mudouPropriedadeDeValidacao);

            using var command = new FbCommand($"update {_nomeTabela} set {item.RecebeColunasIgualValorParaSql()} where id = {item.Id}", connection);

            var resultado = command.ExecuteNonQuery();

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

            using var connection = DataBase.AbreConexao();

            using var command = new FbCommand($"delete from {_nomeTabela} where id = {item.Id}", connection);

            var resultado = command.ExecuteNonQuery();

            if (resultado == 0)
            {
                throw new FalhaEmDeletarException("Erro em deletar : erro no sql!");
            }

        }

        public IEnumerable<T> PesquisaTodos()
        {
            using var connection = DataBase.AbreConexao();

            using var command = new FbCommand($"select * from {_nomeTabela} order by id asc", connection);

            return Pesquisa(command);

        }

        public T PesquisaPorId(int id)
        {
            using var connection = DataBase.AbreConexao();

            using var command = new FbCommand($"select * from {_nomeTabela} where id = {id}", connection);

            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var values = CriaListaDePropriedades(reader);

                return (T)Activator.CreateInstance(typeof(T), values);
            }
            else
            {
                throw new PesquisaSemSucessoException();
            }

        }

        protected IEnumerable<T> Pesquisa(FbCommand command)
        {
            var reader = command.ExecuteReader();

            List<List<object>> objects = new List<List<object>>();

            while (reader.Read())
            {
                var values = CriaListaDePropriedades(reader);
                objects.Add(values);
            }

            return CriaListaDePropriedades(objects);
        }

        protected IEnumerable<T> CriaListaDePropriedades(List<List<object>> objects)
        {
            List<T> contas = new List<T>();

            objects
                .ForEach(x => contas.Add((T)Activator.CreateInstance(typeof(T), x.ToList())));

            return contas;
        }

        protected List<object> CriaListaDePropriedades(FbDataReader reader)
        {
            var values = new object[reader.FieldCount];
            reader.GetValues(values);

            return values.ToList();
        }

        protected abstract void ValidaCondicaoDeInsercao(string validacao);
        protected abstract void ValidaCondicaoAtualizacao(string validacao, bool mudouPropriedadeDeValidacao);
    }
}
