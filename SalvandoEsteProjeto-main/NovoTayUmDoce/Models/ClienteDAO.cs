using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using NovoTayUmDoce.Helpers;
using TayUmDoceProjeto.Conexão;
using TayUmDoceProjeto.Models;
using NovoTayUmDoce.Models;

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
                var enderecoId = new EnderecoDAO().Insert(cliente.Endereco);

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Cliente (nome_cli, cpf_cli, contato_cli, data_nascimento_cli, id_end_fk) " +
                    $"VALUES (@nome, @cpf, @contato, @data_nasc, @id_end)";

                query.Parameters.AddWithValue("@nome", cliente.Nome);
                query.Parameters.AddWithValue("@cpf", cliente.Cpf);     
                query.Parameters.AddWithValue("@contato", cliente.Contato);
                query.Parameters.AddWithValue("@data_nasc", cliente.DataNasc?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@id_end", enderecoId);
             

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erro 3007cliente : Contate o suporte!");
            }
        }

     
    }
}
