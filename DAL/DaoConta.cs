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

        public DaoConta(FbConnection connection)
        {
            _connection = connection;
            connection.Open();
            _transaction = connection.BeginTransaction();
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

        IEnumerable<Conta> IDao<Conta>.GetAll()
        {
            return _commandHelper.ExecuteSearchAll();
        }

        Conta IDao<Conta>.GetByID(int id)
        {
            return _commandHelper.ExecuteSearchById(id);
        }
    }
}
