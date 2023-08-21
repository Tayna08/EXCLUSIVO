using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using NovoTayUmDoce.Helpers;
using TayUmDoceProjeto.Conexão;

namespace TayUmDoceProjeto.Models
{
    class ClienteDAO
    {
        private static Conexao conn;

        public ClienteDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Cliente cliente)
        {
            try
            {

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Cliente (nome_cli, cpf_cli, contato_cli, bairro_cli, cidade_cli, complemento_cli, data_nasc_cli, rua_cli, numero_cli) " +
                    $"VALUES (@nome, @cpf, @contato, @bairro, @cidade, @complemento, @data_nasc, @rua, @numero)";

                query.Parameters.AddWithValue("@nome", cliente.Nome);
                query.Parameters.AddWithValue("@cpf", cliente.Cpf);
                query.Parameters.AddWithValue("@contato", cliente.Contato);
                query.Parameters.AddWithValue("@bairro", cliente.Bairro);
                query.Parameters.AddWithValue("@cidade", cliente.Cidade);
                query.Parameters.AddWithValue("@complemento", cliente.Complemento);
                query.Parameters.AddWithValue("@data_nasc", cliente.DataNasc?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@rua", cliente.Rua);
                query.Parameters.AddWithValue("@numero", cliente.Numero);
             

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erro 3007 : Contate o suporte!");
            }
            finally
            {
                conn.Close();
            }
        }

     
    }
}
