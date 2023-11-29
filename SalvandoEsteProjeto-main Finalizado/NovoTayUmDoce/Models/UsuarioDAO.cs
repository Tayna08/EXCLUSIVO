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

        public Usuario GetByUsuario(string usuarioNome)
        {
            try
            {

                var query = _conn.Query();
                query.CommandText = "SELECT * FROM usuario " +
                    "WHERE usuario_usu = @usuario;";

                query.Parameters.AddWithValue("@usuario", usuarioNome);

                MySqlDataReader reader = query.ExecuteReader();

                Usuario usuario = null;

                while (reader.Read())
                {


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
                _conn.Close();
            }
        }
        public void Insert(Usuario usuario)
        {
            try
            {
                var inserir = _conn.Query();

                inserir.CommandText = "CALL InserirUsuario" +
                    "(@usuarionome, @senha)";

                inserir.Parameters.AddWithValue("@usuarionome", usuario.UsuarioNome);
                inserir.Parameters.AddWithValue("@senha", usuario.Senha);


                var resultado = inserir.ExecuteNonQuery();

                if (resultado == 0)
                {
                    throw new Exception("Ocorreram erros ao salvar as informações");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Usuario> List()
        {
            try
            {
                List<Usuario> list = new List<Usuario>();

                var query = _conn.Query();
                query.CommandText = "SELECT * FROM Usuario";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {


                    var usuario = new Usuario();
                    usuario.Id = reader.GetInt32("id_usu");
                    usuario.UsuarioNome = Helpers.DAOHelper.GetString(reader, "usuario_usu");
                    usuario.Senha = Helpers.DAOHelper.GetString(reader, "senha_usu");
                    list.Add(usuario);
                }
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Login(Usuario usuario)
        {
            try
            {
                var comando = _conn.Query();
                comando.CommandText = "Select count(1) from usuario where (nome_usu= @usuario and senha_usu = @senha)";

                comando.Parameters.AddWithValue("@usuario", usuario.UsuarioNome);
                comando.Parameters.AddWithValue("@senha", usuario.Senha);
                int count = Convert.ToInt32(comando.ExecuteScalar());

                if (count == 1)
                    return true;

                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
