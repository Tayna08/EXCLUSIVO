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
        private  Conexao conn;
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
                            estoque.DataFabricacao = (DateTime)DAOHelper.GetDateTime(reader, "data_fabricacao_est");
                            estoque.Datavalidade = (DateTime)DAOHelper.GetDateTime(reader, "validade_est");
                            estoque.Quantidade = DAOHelper.GetInt(reader, "quantidade_est");
                            estoque.Insumos = DAOHelper.GetString(reader, "insumos_est");
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

                            var produto = new Produto();

                            produto.Id = reader.GetInt32("id_pro");
                            produto.Nome = reader.GetString("nome_pro");

                            var estoque = new Estoque()
                            {

                                Id = DAOHelper.GetInt(reader, "id_est"),
                                Quantidade = DAOHelper.GetInt(reader, "quantidade_est"),
                                Datavalidade = DAOHelper.GetDateTime(reader, "validade_est"),
                                DataFabricacao = DAOHelper.GetDateTime(reader, "data_fabricacao_est"),
                                Insumos = DAOHelper.GetString(reader, "insumos_est"),
                                

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

        public string Insert(Estoque estoque)
        {
            try
            {

                    var query = conn.Query();
                    query.CommandText = $"INSERT INTO Estoque (quantidade_est, validade_est,  data_fabricacao_est, insumos_est, id_pro_fk) " +
                        $"VALUES (@quantidade, @validade, @data_fabricacao, @insumos, @id_pro)";

                    query.Parameters.AddWithValue("@quantidade", estoque.Quantidade);
                    query.Parameters.AddWithValue("@validade", estoque.Datavalidade?.ToString("yyyy-MM-dd"));
                    query.Parameters.AddWithValue("@data_fabricacao", estoque.DataFabricacao?.ToString("yyyy-MM-dd"));
                    query.Parameters.AddWithValue("@insumos", estoque.Insumos);
                    query.Parameters.AddWithValue("@id_pro", estoque.Produto.Id);

                var resultado = (string)query.ExecuteScalar();
                return resultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir os dados: " + ex.Message);
                throw;
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
          
        }

        public void Update(Estoque estoque)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE estoque set quantidade_est = @quantidade, validade_est = @validade, data_fabricacao_est = @data_fabricacao, insumos_est = @insumos, id_pro_fk = @id_pro WHERE id_est = @id";
                query.Parameters.AddWithValue("@quantidade", estoque.Quantidade);
                query.Parameters.AddWithValue("@validade", estoque.Datavalidade?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@data_fabricacao", estoque.DataFabricacao?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@insumos", estoque.Insumos);
                query.Parameters.AddWithValue("@id_pro", estoque.Produto.Id);

                query.Parameters.AddWithValue("@id", estoque.Id);

                var resultado = query.ExecuteNonQuery();
                if (resultado == 0)
                    throw new Exception("Atualização do registro não foi realizada.");

            }
            catch (Exception e)
            {
                MessageBox.Show($"Erro ao atualizar estoque: {e.Message}");

                throw;
            }
        }   


    }
}