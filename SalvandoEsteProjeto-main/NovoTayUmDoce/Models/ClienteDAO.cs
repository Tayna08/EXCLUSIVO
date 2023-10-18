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

        public List<Cliente> List()
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "SELECT * FROM cliente LEFT JOIN endereco ON id_end = id_end_fk";
                var reader = query.ExecuteReader();

                var lista = new List<Cliente>();

                while (reader.Read())
                {

                    var endereco = new Endereco()
                    {
                        Id = reader.GetInt32("id_end"),
                        Bairro = reader.GetString("bairro_end"),
                        Cidade = reader.GetString("cidade_end"),
                        Rua = reader.GetString("rua_end"),
                        Complemento = reader.GetString("complemento_end"),
                        Numero = reader.GetInt32("numero_end"),
                        Cep = reader.GetString("cep_end")
                    };

                    var data = reader.GetDateTime("data_nascimento_cli").ToString("dd/MM/yyyy");

                    var cliente = new Cliente()
                    {
                        Id = reader.GetInt32("id_cli"),
                        Nome = reader.GetString("nome_cli"),
                        Cpf = reader.GetString("cpf_cli"),
                        Contato = reader.GetString("contato_cli"),
                        DataNasc = reader.GetDateTime(data),
                        Endereco = endereco,
                    };

                    lista.Add(cliente);
                }

                return lista;
            }
            catch (Exception e) 
            {
                throw e; 
            }
        }

        public void Insert(Cliente cliente)
        {
            try
            {
                var enderecoId = new EnderecoDAO().Insert(cliente.Endereco);

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Cliente (nome_cli, cpf_cli, data_nascimento_cli, contato_cli, id_end_fk) " +
                    $"VALUES (@nome, @cpf, @data_nasc, @contato, @id_end)";

                query.Parameters.AddWithValue("@nome", cliente.Nome);
                query.Parameters.AddWithValue("@cpf", cliente.Cpf);     
                query.Parameters.AddWithValue("@data_nasc", cliente.DataNasc?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@contato", cliente.Contato);
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
                MessageBox.Show("Erro 3007 : Contate o suporte!");
            }
        }

     
    }
}
