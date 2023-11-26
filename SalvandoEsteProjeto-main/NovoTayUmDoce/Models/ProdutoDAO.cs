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
                                produto.Tipo = DAOHelper.GetString(reader, "tipo_pro");
                                produto.Descricao = DAOHelper.GetString(reader, "descricao_pro");
                                produto.Valor_Venda = DAOHelper.GetDouble(reader, "valor_venda_pro");

                            }

                            lista.Add(produto);
                        }

                        reader.Close();

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

        public long Insert(Produto produto)
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
                        

                        var result = query.ExecuteNonQuery();

                        if (result == 0)
                        {
                            MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
                        }
                        else
                        {
                            MessageBox.Show("Inserção bem-sucedida!");
                        }

                return query.LastInsertedId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erro 3007 : Contate o suporte!");
                return 0;
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
         
        }
    }
}
