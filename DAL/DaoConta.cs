using Banco.Data;
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

        public Conta GetByCpf(string cpf)
        {
            var connection = new DataBase().AbrirConexao();

            var command = new FbCommand($"select * from {_nomeTabela} where con_cpf = \'{cpf}\'", connection);

            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var values = CreateArrayOfValues(reader);

                connection.Close();

                var conta = new Conta();
                return (Conta)conta.SetPropertiesFromObjectArray(values);
            }
            else
            {
                connection.Close();

                throw new PesquisaSemSucessoException();
            }
        }

        protected override void ValidaCondicaoInsert(string validacao)
        {
            string cpf = validacao;

            try
            {
                var conta = GetByCpf(cpf);

                //Só passa dessa linha se achar um item com aquele cpf
                throw new CpfExistenteException(cpf);
            }
            catch (PesquisaSemSucessoException)
            {
                //Significa que não achou, ou seja, pode continuar a inserção
            }

            if (!ValidaCPF.IsCpf(cpf))
            {
                throw new CpfInvalidoException(cpf);
            }
        }

        protected override void ValidaCondicaoUpdate(string validacao, bool mudouPropriedadeDeValidacao)
        {
            string cpf = validacao;

            try
            {
                var conta = GetByCpf(cpf);

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

            if (!ValidaCPF.IsCpf(cpf))
            {
                throw new CpfInvalidoException(cpf);
            }
        }
    }
}
