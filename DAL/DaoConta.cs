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
    class DaoConta : IDao<Conta>
    {
        FbConnection _connection = null;
        FbTransaction _transaction = null;
        CommandHelper<Conta> _commandHelper;
        private readonly string _nomeTabela = "contas";

        public DaoConta()
        {
            _connection = DataBase.GetConexao();
            _transaction = DataBase.GetTransaction();
            _commandHelper = new CommandHelper<Conta>(_nomeTabela, _connection, _transaction);
        }

        public void Insert(Conta item)
        {
            _commandHelper.ExecuteInsert(item);
        }
        public void Update(Conta item)
        {
            _commandHelper.ExecuteUpdate(item);
        }
        public void Delete(Conta item)
        {
            _commandHelper.ExecuteSearchById(item.Id);

            _commandHelper.ExecuteDelete(item.Id);
        }

        public IEnumerable<Conta> GetAll()
        {
            return _commandHelper.ExecuteSearchAll();
        }

        public Conta GetByID(int id)
        {
            return _commandHelper.ExecuteSearchById(id);
        }

        public Conta GetByCpf(string cpf)
        {
            return _commandHelper.ExecuteSearchByCpf(cpf);
        }
    }
}
