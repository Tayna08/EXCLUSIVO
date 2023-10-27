using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovoTayUmDoce.Conexão;
using System.Windows;
using NovoTayUmDoce.Models;
using NovoTayUmDoce.Helpers;
using MySql.Data.MySqlClient;

namespace NovoTayUmDoce.Models
{
    class FornecedorDAO
    {
        private static Conexao conn;


        public FornecedorDAO()
        {
            conn = new Conexao();
        }

        public Fornecedor GetById (int id)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM Fornecedor where (id_for = @id)";
                    query.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = query.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Nenhum fornecedor foi encontrado!");
                            return null;
                        }

                        var fornecedor = new Fornecedor();

                        while (reader.Read())
                        {
                            fornecedor.Id = DAOHelper.GetInt(reader, "id_for");
                            fornecedor.Nome_Fantasia = DAOHelper.GetString(reader, "nome_fantasia_for");
                            fornecedor.Nome_Representante = DAOHelper.GetString(reader, "nome_representante_for");
                            fornecedor.Contato = DAOHelper.GetString(reader, "contato_for");
                            fornecedor.Cnpj = DAOHelper.GetString(reader, "cnpj_for");
                            fornecedor.Razao_Social = DAOHelper.GetString(reader, "razao_social_for");
                            fornecedor.Email = DAOHelper.GetString(reader, "email_for");
                            fornecedor.Endereco = new EnderecoDAO().GetById(DAOHelper.GetInt(reader, "id_end_fk"));
                           // fornecedor.Estoque = new EstoqueDAO().GetById(DAOHelper.GetInt(reader, "id_est_fk"));
                        }

                        return fornecedor;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public List<Fornecedor> List()
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "SELECT * FROM fornecedor LEFT JOIN endereco ON id_end = id_end_fk";
                var reader = query.ExecuteReader();

                var lista = new List<Fornecedor>();

                while (reader.Read())
                {
                   /* var estoque = new Estoque();
                    {
                        Id = reader.GetInt32("id_est");
                        Nome = reader.GetString("nome_est");
                        Quantidade = reader.GetInt32("quantidade_est");
                        Data = reader.GetDateTime("data_est");
                    };*/

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

                    var fornecedor = new Fornecedor()
                    {
                        Id = reader.GetInt32("id_for"),
                        Nome_Fantasia = reader.GetString("nome_fantasia_for"),
                        Nome_Representante = reader.GetString("nome_representante_for"),
                        Contato = reader.GetString("contato_for"),
                        Cnpj= reader.GetString("cnpj_for"),
                        Razao_Social= reader.GetString("razao_social_for"),
                        Email = reader.GetString("email_for"),
                        Endereco = endereco,
                    };

                    lista.Add(fornecedor);
                }

                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void Insert(Fornecedor fornecedor)
        {
            try
            {
                var enderecoId = new EnderecoDAO().Insert(fornecedor.Endereco);

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Fornecedor (nome_fantasia_for, nome_Representante_for, contato_for, cnpj_for, razao_social_for, email_for id_end_fk, id_est_fk)" +
                    $"VALUES (@nome_fantasia, @nome_representante, @contato, @cnpj, @razao_social, @email, @id_end, @id_est_fk)";

                query.Parameters.AddWithValue("@nome_fantasia", fornecedor.Nome_Fantasia);
                query.Parameters.AddWithValue("@nome_representante", fornecedor.Nome_Representante);
                query.Parameters.AddWithValue("@contato", fornecedor.Contato);
                query.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
                query.Parameters.AddWithValue("@razao_social", fornecedor.Razao_Social);
                query.Parameters.AddWithValue("@email", fornecedor.Email);
                query.Parameters.AddWithValue("@id_end", enderecoId);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
                }
                else
                {
                    MessageBox.Show("Inserção bem-sucedida!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir os dados: " + ex.Message);
            }

        }

        public void Delete(Fornecedor fornecedor)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "delete from Fornecedor where (id_for = @id)";

                query.Parameters.AddWithValue("@id", fornecedor.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover o fornecedor. Verifique e tente novamente.");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
