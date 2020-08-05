using Banco.Exceptions;
using Banco.Model;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Util
{
    class CommandHelperConta : CommandHelper<Conta>
    {
        public CommandHelperConta(string tabela, FbConnection connection, FbTransaction transaction) : base(tabela, connection, transaction)
        {
        }

        public override void ExecuteInsert(Conta item)
        {
            ValidaCpf(item.Cpf);

            _command = new FbCommand($"insert into {_tabela} {item.GetNameOfTableColumns()} values {item.GetValueOfTableProperties()}", _connection, _transaction);

            var resultado = _command.ExecuteNonQuery();

            if (resultado == 0)
            {
                throw new FalhaEmInserirException();
            }
        }

        public override void ExecuteUpdate(Conta item)
        {
            ValidaCpf(item.Cpf);

            _command = new FbCommand($"update {_tabela} set {item.GetColumnEqualsValue()} where id = id", _connection, _transaction);

            var resultado = _command.ExecuteNonQuery();

            if (resultado == 0)
            {
                throw new FalhaEmAtualizarException();
            }
        }

        public Conta ExecuteSearchByCpf(string cpf)
        {
            _command = new FbCommand($"select * from {_tabela} where con_cpf = \'{cpf}\'", _connection, _transaction);

            var reader = _command.ExecuteReader();

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
                var conta = ExecuteSearchByCpf(cpf);

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
