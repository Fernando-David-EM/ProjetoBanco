using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Data
{
    class DataBase
    {
		private static FbConnection _conexao = null;

		private DataBase()
        {

        }

		public static FbConnection GetConexao()
        {
			if (_conexao == null)
            {
				_conexao = new FbConnection("database=localhost:banco.fdb;user=sysdba;password=@Fernando23");
			}

			return _conexao;
        }
    }
}
