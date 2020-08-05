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

        public override void Insert(Conta item)
        {
            ValidaCpf(item.Cpf);

            var command = new FbCommand($"insert into {_nomeTabela} {item.GetNameOfTableColumns()} values {item.GetValueOfTableProperties()}", _connection, _transaction);

            var resultado = command.ExecuteNonQuery();

            if (resultado == 0)
            {
                throw new FalhaEmInserirException();
            }
        }

        public override void Update(Conta item)
        {
            ValidaCpf(item.Cpf);

            var command = new FbCommand($"update {_nomeTabela} set {item.GetColumnEqualsValue()} where id = id", _connection, _transaction);

            var resultado = command.ExecuteNonQuery();

            if (resultado == 0)
            {
                throw new FalhaEmAtualizarException();
            }
        }

        public Conta GetByCpf(string cpf)
        {
            var command = new FbCommand($"select * from {_nomeTabela} where con_cpf = \'{cpf}\'", _connection, _transaction);

            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var values = CreateArrayOfValues(reader);
                var conta = new Conta();
                return (Conta)conta.SetPropertiesFromObjectArray(values);
            }
            else
            {
                throw new PesquisaSemSucessoException();
            }
        }

        private void ValidaCpf(string cpf)
        {
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
    }
}
