using FirebirdSql.Data.FirebirdClient;

namespace Banco.Data
{
    class DataBase 
    {
        public static FbConnection AbreConexao()
        {
            var conexao = new FbConnection(
                @"ServerType=0;
                    database=localhost:E:\Prog\EM\ProjetoBanco\BANCO.FDB;
                    user=SYSDBA;
                    password=@Fernando23");

            conexao.Open();

            return conexao;
        }
    }
}
