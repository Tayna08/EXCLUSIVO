using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovoTayUmDoce.Conexão;
using System.Windows;
using NovoTayUmDoce.Helpers;
using MySql.Data.MySqlClient;

namespace NovoTayUmDoce.Models
{
    class ProdutoDAO
    {
        private Conexao conn;


        public ProdutoDAO()
        {
            conn = new Conexao();
        }
        public Produto GetById(int id)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM Produto WHERE (id_pro = @id)";
                    query.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = query.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Nenhum produto foi encontrado!");
                            return null;
                        }

                        var produto = new Produto();

                        while (reader.Read())
                        {
                            produto.Id = reader.GetInt32("id_pro");
                            produto.Nome = reader.GetString("nome_pro");
                            produto.Peso = reader.GetString("peso_pro");
                            produto.Tipo = reader.GetString("tipo_pro");
                            produto.Descricao = reader.GetString("descricao_pro");
                            produto.Valor_Venda = reader.GetDouble("valor_venda_pro");
                            
                           
                        }

                        return produto;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        public List<Produto> List()
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM Produto";
                    using ( var reader = query.ExecuteReader())
                    {
                        var lista = new List<Produto>();

                        while (reader.Read())
                        {
                            var produto = new Produto()
                            {
                                Id = DAOHelper.GetInt(reader, "id_pro"),
                                Nome = DAOHelper.GetString(reader, "nome_pro"),
                                Peso = DAOHelper.GetString(reader, "peso_pro"),
                                Tipo = DAOHelper.GetString(reader, "tipo_pro"),
                                Descricao = DAOHelper.GetString(reader, "descricao_pro"),
                                Valor_Venda = DAOHelper.GetDouble(reader, "valor_venda_pro"),

                            };

                            lista.Add(produto);
                        }

                        return lista;
                    }
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show($"Erro ao listar os produtos: {e.Message}");
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public string Insert(Produto produto)
        {
            try
            {

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Produto (nome_pro, peso_pro, tipo_pro, descricao_pro, valor_venda_pro ) " +
                            $"VALUES (@nome, @peso, @tipo, @descricao, @valor)";

                        query.Parameters.AddWithValue("@nome", produto.Nome);
                        query.Parameters.AddWithValue("@peso", produto.Peso);
                        query.Parameters.AddWithValue("@tipo", produto.Tipo);
                        query.Parameters.AddWithValue("@descricao", produto.Descricao);
                        query.Parameters.AddWithValue("@valor", produto.Valor_Venda);


                        var resultado = (string)query.ExecuteScalar();
                        return resultado;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inserir cliente: {ex.Message}");
                throw;
            }

        }
        public void Delete(Produto produto)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "DELETE FROM Produto WHERE (id_pro = @id)";

                    query.Parameters.AddWithValue("@id", produto.Id);

                    var result = query.ExecuteNonQuery();

                    if (result == 0)
                    {
                        MessageBox.Show("Erro ao remover o produto. Verifique e tente novamente.");
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
        public void Update(Produto produto)
        {
            try
            {
              
                var query = conn.Query();
                query.CommandText = "UPDATE Produto SET nome_pro = @nome, peso_pro = @peso, tipo_pro = @tipo, " +
                                     "descricao_pro = @descricao, valor_venda_pro = @valor_venda WHERE id_pro = @id";

                query.Parameters.AddWithValue("@nome", produto.Nome);
                query.Parameters.AddWithValue("@peso", produto.Peso);
                query.Parameters.AddWithValue("@tipo", produto.Tipo);
                query.Parameters.AddWithValue("@descricao", produto.Descricao);
                query.Parameters.AddWithValue("@valor_venda", produto.Valor_Venda);
                query.Parameters.AddWithValue("@id", produto.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Atualização do registro não foi realizada.");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Erro ao atualizar produto: {e.Message}");
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
