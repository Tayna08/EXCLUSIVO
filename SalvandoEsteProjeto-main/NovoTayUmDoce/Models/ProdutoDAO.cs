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
        private static Conexao conn;


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
                            produto.Valor_Gasto = reader.GetDouble("valor_gasto_pro");
                            produto.Valor_Venda = reader.GetDouble("valor_venda_pro");
                            produto.Data = reader.GetDateTime("data_fabricacao_pro");
                            produto.Estoque_medio = reader.GetString("estoque_medio");
                            produto.Estoque_maximo = reader.GetString("estoque_maximo");
                            produto.Quantidade = reader.GetInt32("quantidade_pro");
                            produto.Tipo = reader.GetString("tipo_pro");
                            produto.Descricao = reader.GetString("descricao_pro");
                            
                           
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
                            var produto = new Produto();
                            {
                                produto.Id = DAOHelper.GetInt(reader, "id_pro");
                                produto.Nome = DAOHelper.GetString(reader, "nome_pro");
                                produto.Peso = DAOHelper.GetString(reader, "peso_pro");
                                produto.Valor_Gasto = DAOHelper.GetDouble(reader, "valor_gasto_pro");
                                produto.Valor_Venda = DAOHelper.GetDouble(reader, "valor_venda_pro");
                                produto.Data = DAOHelper.GetDateTime(reader, "data_fabricacao_pro");
                                produto.Estoque_medio = DAOHelper.GetString(reader, "estoque_medio");
                                produto.Estoque_maximo = DAOHelper.GetString(reader, "estoque_maximo");
                                produto.Quantidade = DAOHelper.GetInt(reader, "quantidade_pro");
                                produto.Tipo = DAOHelper.GetString(reader, "tipo_pro");
                                produto.Descricao = DAOHelper.GetString(reader, "descricao_pro");

                            }


                            lista.Add(produto);
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

        public void Insert(Produto produto)
        {
            try
            {

                var query = conn.Query();

                query.CommandText = $"INSERT INTO Produto (nome_pro, peso_pro, valor_gasto_pro, valor_venda_pro, data_fabricacao_pro, estoque_medio, estoque_maximo, quantidade_pro, tipo_pro, descricao_pro ) " +
                            $"VALUES (@nome, @peso, @valor_gasto, @valor_venda, @data_fabricacao, @estoque_medio, @estoque_maximo, @quantidade, @tipo, @descricao)";

                        query.Parameters.AddWithValue("@nome", produto.Nome);
                        query.Parameters.AddWithValue("@peso", produto.Peso);
                        query.Parameters.AddWithValue("@valor_gasto", produto.Valor_Gasto);
                        query.Parameters.AddWithValue("@valor_venda", produto.Valor_Venda);
                        query.Parameters.AddWithValue("@data_fabricacao", produto.Data?.ToString("yyyy-MM-dd"));
                        query.Parameters.AddWithValue("@estoque_medio", produto.Estoque_medio);
                        query.Parameters.AddWithValue("@estoque_maximo", produto.Estoque_maximo);
                        query.Parameters.AddWithValue("@quantidade", produto.Quantidade);
                        query.Parameters.AddWithValue("@tipo", produto.Tipo);
                        query.Parameters.AddWithValue("@descricao", produto.Descricao);
                        

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
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erro 3007 : Contate o suporte!");
            }
          
        }
        public void Delete(Produto produto)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "DELETE FROM produto WHERE (id_pro = @id)";

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
         
        }
    }
}
