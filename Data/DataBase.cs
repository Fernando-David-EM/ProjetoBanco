using FirebirdSql.Data.FirebirdClient;

namespace Banco.Data
{
    class DataBase 
    {
        public FbConnection AbrirConexao()
        {
            var conexao = new FbConnection(
                @"ServerType=0;
                    database=localhost:C:\Users\Escolar Manager\source\repos\Banco\BANCO.FDB;
                    user=SYSDBA;
                    password=@Fernando23");

            conexao.Open();

            return conexao;
        }
    }
}
