using MySql.Data.MySqlClient;
using NovoTayUmDoce.Conexão;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovoTayUmDoce.Models
{
    class UsuarioDAO : AbstractDAO<Usuario>
    {


        private static Conexao _conn = new Conexao();

        public Usuario GetByUsuario(string usuarioNome, string senha)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "SELECT * FROM usuario LEFT JOIN funcionario ON id_fun = id_fun_fk " +
                    "WHERE usuario_usu = @usuario AND senha_usu = @senha";

                query.Parameters.AddWithValue("@usuario", usuarioNome);
                query.Parameters.AddWithValue("@senha", senha);

                MySqlDataReader reader = query.ExecuteReader();

                Usuario usuario = null;

                while (reader.Read())
                {
                    usuario = Usuario.GetInstance();
                    usuario.Id = reader.GetInt32("id_usu");
                    usuario.UsuarioNome = reader.GetString("usuario_usu");
                    usuario.Funcionario = new Funcionario() { Id = reader.GetInt32("id_fun"), Nome = reader.GetString("nome_fun") };
                }

                return usuario;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
