using FirebirdSql.Data.FirebirdClient;

namespace Banco.Data
{
    class DataBase
    {
		private static FbConnection _conexao;
        private static FbTransaction _transaction;

		private DataBase()
        {

        }

		public static FbConnection GetConexao()
        {
			if (_conexao == null)
            {
				_conexao = new FbConnection(
                    @"ServerType=0;
                    database=localhost:C:\Users\Escolar Manager\source\repos\Banco\BANCO.FDB;
                    user=SYSDBA;
                    password=@Fernando23");

                _conexao.Open();
            }

            return _conexao;
        }

        public static FbTransaction GetTransaction()
        {
            if (_conexao != null)
            {
                if (_transaction == null)
                {
                    _transaction = _conexao.BeginTransaction();
                }
            }
            else
            {
                GetConexao();
                GetTransaction();
            }

            return _transaction;
        }
    }
}
