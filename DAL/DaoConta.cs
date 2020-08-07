using Banco.Data;
using Banco.Exceptions;
using Banco.Model;
using Banco.Util;
using FirebirdSql.Data.FirebirdClient;
using System.Data;
using System.Windows.Forms;

namespace Banco.DAL
{
    class DaoConta : Dao<Conta>
    {
        public DaoConta() : base("contas")
        {

        }

        public Conta PesquisaPorCpf(string cpf)
        {
            using var connection = DataBase.AbreConexao();

            using var command = new FbCommand($"select * from {_nomeTabela} where con_cpf = \'{cpf}\'", connection);

            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var propriedades = GeraPropriedadesParaConstrutor(reader);

                return InstanciaObjeto(propriedades);
            }
            else
            {
                throw new PesquisaSemSucessoException();
            }

        }

        protected override void VerificaCondicao(Conta item)
        {
            var conta = PesquisaPorCpf(item.Cpf);

            if (item.Id != conta.Id)
            {
                throw new CpfExistenteException(item.Cpf);
            }
        }
    }
}
