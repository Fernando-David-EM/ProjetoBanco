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
                var values = GeraPropriedadesParaConstrutor(reader);

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

        protected override void VerificaCondicao(Login item)
        {
            var login = PesquisaPorUsuario(item.Usuario);

            if (login.Id != item.Id)
            {
                throw new UsuarioExistenteException(item.Usuario);
            }
        }
    }
}
