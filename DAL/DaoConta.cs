﻿using Banco.Data;
using Banco.Exceptions;
using Banco.Model;
using Banco.Util;
using FirebirdSql.Data.FirebirdClient;

namespace Banco.DAL
{
    class DaoConta : Dao<Conta>
    {
        public DaoConta() : base("contas")
        {
            
        }

        public Conta PesquisaPorCpf(string cpf)
        {
            var connection = DataBase.AbreConexao();

            var command = new FbCommand($"select * from {_nomeTabela} where con_cpf = \'{cpf}\'", connection);

            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var values = CriaArrayDePropriedades(reader);

                connection.Close();

                var conta = new Conta();
                return (Conta)conta.RecebeContaComPropriedadesDeCampos(values);
            }
            else
            {
                connection.Close();

                throw new PesquisaSemSucessoException();
            }
        }

        protected override void ValidaCondicaoDeInsercao(string validacao)
        {
            string cpf = validacao;

            try
            {
                var conta = PesquisaPorCpf(cpf);

                //Só passa dessa linha se achar um item com aquele cpf
                throw new CpfExistenteException(cpf);
            }
            catch (PesquisaSemSucessoException)
            {
                //Significa que não achou, ou seja, pode continuar a inserção
            }

            if (!ValidaCPF.EhCpf(cpf))
            {
                throw new CpfInvalidoException(cpf);
            }
        }

        protected override void ValidaCondicaoAtualizacao(string validacao, bool mudouPropriedadeDeValidacao)
        {
            string cpf = validacao;

            try
            {
                var conta = PesquisaPorCpf(cpf);

                //Só passa dessa linha se achar um item com aquele cpf
               if (mudouPropriedadeDeValidacao)
                {
                    throw new CpfExistenteException(cpf);
                }
            }
            catch (PesquisaSemSucessoException)
            {
                //Significa que não achou, ou seja, pode continuar a inserção
            }

            if (!ValidaCPF.EhCpf(cpf))
            {
                throw new CpfInvalidoException(cpf);
            }
        }
    }
}
