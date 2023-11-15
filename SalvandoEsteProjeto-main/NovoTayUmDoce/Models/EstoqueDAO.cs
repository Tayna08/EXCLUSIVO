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
                            estoque.DataFabricacao = (DateTime)DAOHelper.GetDateTime(reader, "data_fabricacao_est");
                            estoque.Datavalidade = (DateTime)DAOHelper.GetDateTime(reader, "validade_ent");
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

        public void Insert(Estoque estoque)
        {
            try
            {
                var produtoId = new ProdutoDAO().GetById(estoque.Produto.Id);

                if (produtoId.Id > 0)
                {
                    var query = conn.Query();
                    query.CommandText = $"INSERT INTO Estoque (quantidade_est, validade_est,  data_fabricacao_est, insumos_est, id_pro_fk) " +
                        $"VALUES (@quantidade, @validade, @data_fabricacao, @insumos, @id_pro)";


                    query.Parameters.AddWithValue("@quantidade", estoque.Quantidade);
                    query.Parameters.AddWithValue("@validade", estoque.Datavalidade?.ToString("yyyy-MM-dd"));
                    query.Parameters.AddWithValue("@data_fabricacao", estoque.DataFabricacao?.ToString("yyyy-MM-dd"));
                    query.Parameters.AddWithValue("@insumos", estoque.Insumos);
                    query.Parameters.AddWithValue("@id_pro", produtoId.Id);

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