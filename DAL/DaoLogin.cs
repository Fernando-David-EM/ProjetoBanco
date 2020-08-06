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

        public Login GetByUsuario(string usuario)
        {
            var connection = new DataBase().AbrirConexao();

            var command = new FbCommand($"select * from {_nomeTabela} where log_usuario = \'{usuario}\'", connection);

            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var values = CreateArrayOfValues(reader);

                connection.Close();

                var conta = new Login();
                return (Login)conta.SetPropertiesFromObjectArray(values);
            }
            else
            {
                connection.Close();

                throw new PesquisaSemSucessoException();
            }
        }

        public Login Logar(string usuario, string senha)
        {
            try
            {
                var login = GetByUsuario(usuario);

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

        protected override void ValidaCondicaoInsert(string validacao)
        {
            string usuario = validacao;

            try
            {
                var login = GetByUsuario(usuario);

                //Só passa dessa linha se achar um item com aquele usuario
                throw new UsuarioExistenteException(usuario);
            }
            catch (PesquisaSemSucessoException)
            {
                //Significa que não achou, ou seja, pode continuar a inserção
            }
        }

        protected override void ValidaCondicaoUpdate(string validacao, bool mudouPropriedadeDeValidacao)
        {
            string usuario = validacao;

            try
            {
                var login = GetByUsuario(usuario);

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
