using Banco.Data;
using Banco.Exceptions;
using Banco.Model;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.DAL
{
    class DaoLogin : Dao<Login>
    {
        public DaoLogin() : base("login")
        {
        }

        public Login PesquisaPorUsuario(string usuario)
        {
            using var connection = DataBase.AbreConexao();

            using var command = new FbCommand($"select * from {_nomeTabela} where log_usuario = \'{usuario}\'", connection);

            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var values = CriaListaDePropriedades(reader);

                return new Login(values);
            }
            else
            {
                throw new PesquisaSemSucessoException();
            }

        }

        public Login Logar(string usuario, string senha)
        {
            try
            {
                var login = PesquisaPorUsuario(usuario);

                if (login.Senha == senha)
                {
                    return login;
                }
                else
                {
                    throw new SenhaIncorretaException();
                }
            }
            catch (PesquisaSemSucessoException)
            {

                throw new UsuarioInexistenteException();
            }


        }

        protected override void ValidaCondicaoDeInsercao(string validacao)
        {
            string usuario = validacao;

            try
            {
                var login = PesquisaPorUsuario(usuario);

                //Só passa dessa linha se achar um item com aquele usuario
                throw new UsuarioExistenteException(usuario);
            }
            catch (PesquisaSemSucessoException)
            {
                //Significa que não achou, ou seja, pode continuar a inserção
            }
        }

        protected override void ValidaCondicaoAtualizacao(string validacao, bool mudouPropriedadeDeValidacao)
        {
            string usuario = validacao;

            try
            {
                var login = PesquisaPorUsuario(usuario);

                //Só passa dessa linha se achar um item com aquele usuario
                if (mudouPropriedadeDeValidacao)
                {
                    throw new UsuarioExistenteException(usuario);
                }
            }
            catch (PesquisaSemSucessoException)
            {
                //Significa que não achou, ou seja, pode continuar a inserção
            }
        }
    }
}
