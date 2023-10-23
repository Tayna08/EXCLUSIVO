using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace TayUmDoceProjeto.Conexão
{
    class Conexao
    {
        private static string host = "localhost";

        private static string port = "3306";

        private static string user = "root";

        private static string password = "root";

        private static string dbname = "projeto_tay_bd";

        private static MySqlConnection connection;

        private static MySqlCommand cmd;

        public Conexao()
        {
            try
            {
                connection = new MySqlConnection($"server={host};database={dbname};port={port};user={user};password={password}");
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3306 : Contate o suporte");
                throw ex;
            }
        }

        public MySqlCommand Query()
        {
            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                return cmd;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3306 : Contate o suporte");
                throw ex;
            }
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
