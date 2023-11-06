using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovoTayUmDoce.Conexão;
using System.Windows;
using MySql.Data.MySqlClient;
using NovoTayUmDoce.Helpers;

namespace NovoTayUmDoce.Models
{
    class EstoqueDAO
    {
        private static Conexao conn;
        public EstoqueDAO()
        {
            conn = new Conexao();
        }

        public Estoque GetById(int id)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM Estoque WHERE (id_est = @id)";
                    query.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = query.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Nenhum estoque foi encontrado!");
                            return null;
                        }

                        var estoque = new Estoque();

                        while (reader.Read())
                        {
                            estoque.Id = DAOHelper.GetInt(reader, "id_est");
                            estoque.Data = (DateTime)DAOHelper.GetDateTime(reader, "data_est");
                            estoque.validade = (DateTime)DAOHelper.GetDateTime(reader, "validade_ent");
                            estoque.Quantidade = DAOHelper.GetInt(reader, "quantidade_est");
                            estoque.Produto = new ProdutoDAO().GetById(DAOHelper.GetInt(reader, "id_pro_fk"));
                        }

                        return estoque;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public List<Estoque> List()
        {
            try
            {

                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM estoque LEFT JOIN produto ON id_pro = id_pro_fk";
                    using (var reader = query.ExecuteReader())
                    {
                        var lista = new List<Estoque>();

                        while (reader.Read())
                        {

                            var estoque = new Estoque()
                            {

                                Id = DAOHelper.GetInt(reader, "id_est"),
                                Quantidade = DAOHelper.GetInt(reader, "quantidade_est"),
                                Data = DAOHelper.GetDateTime(reader, "data_est"),
                                validade = DAOHelper.GetDateTime(reader, "validade_est"),

                            };

                            lista.Add(estoque);
                        }

                        return lista;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }
        public void Insert(Estoque estoque)
        {
            try
            {
                var produtoId = new ProdutoDAO().GetById(estoque.Produto.Id);

                if (produtoId.Id > 0)
                {
                    var query = conn.Query();
                    query.CommandText = $"INSERT INTO Estoque (nome_est, quantidade_est,validade_est, data_est, id_pro_fk) " +
                        $"VALUES (@nome, @quantidade, @validade, @data, @id_pro)";


                    query.Parameters.AddWithValue("@quantidade", estoque.Quantidade);
                    query.Parameters.AddWithValue("@data", estoque.Data?.ToString("yyyy-MM-dd"));
                    query.Parameters.AddWithValue("@validade", estoque.Data?.ToString("yyyy-MM-dd"));
                    query.Parameters.AddWithValue("@id_pro", produtoId.Id);



                    var result = query.ExecuteNonQuery();

                    if (result == 0)
                    {
                        MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erro 3007 : Contate o suporte!");
            }

         
        }
        public void Delete(Estoque estoque)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "DELETE FROM estoque WHERE (id_est = @id)";

                    query.Parameters.AddWithValue("@id", estoque.Id);

                    var result = query.ExecuteNonQuery();

                    if (result == 0)
                    {
                        MessageBox.Show("Erro ao remover o estoque. Verifique e tente novamente.");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
