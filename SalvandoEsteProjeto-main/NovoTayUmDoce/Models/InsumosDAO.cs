using MySql.Data.MySqlClient;
using NovoTayUmDoce.Conexão;
using NovoTayUmDoce.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NovoTayUmDoce.Models
{
    internal class InsumosDAO
    {
        private static Conexao conn;


        public InsumosDAO()
        {
            conn = new Conexao();
        }
        public Insumos GetById(int id)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM Insumo WHERE (id_ins = @id)";
                    query.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = query.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Nenhum Insumo foi encontrado!");
                            return null;
                        }

                        var insumos = new Insumos();

                        while (reader.Read())
                        {
                            insumos.Id = reader.GetInt32("id_ins");
                            insumos.Nome = reader.GetString("nome_ins");
                            insumos.Peso = reader.GetString("peso_ins");
                            insumos.Valor_Gasto = reader.GetDouble("valor_gasto_ins");
                            insumos.Estoque_medio = reader.GetString("estoque_medio_ins");
                            insumos.Estoque_maximo = reader.GetString("estoque_maximo_ins");
                           

                        }

                        return insumos;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
        public List<Insumos> List()
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM insumos LEFT JOIN fornecedor ON id_for = id_for_fk";
                    using (var reader = query.ExecuteReader())
                    {
                        var lista = new List<Insumos>();

                        while (reader.Read())
                        {
                            var fornecedor = new Fornecedor()
                            {

                                Id = DAOHelper.GetInt(reader, "id_for"),
                               

                            };
                            var produto = new Produto()
                            {
                                Id = DAOHelper.GetInt(reader, "id_pro"),
                            };

                            //lista.Add(produto);
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

        public void Insert(Insumos insumos)
        {
            try
            {
                var fornecedorid = new PedidoDAO().GetById(insumos.Fornecedor.Id);

                if (fornecedorid.Id > 0)
                {
                    using (var query = conn.Query())
                    {

                        query.CommandText = $"INSERT INTO Produto (nome_pro, peso_pro, valor_gasto_pro, estoque_medio, estoque_maximo, id_for_fk ) " +
                            $"VALUES (@nome, @peso, @valor_gasto, @estoque_medio, @estoque_maximo, @id_for)";

                        query.Parameters.AddWithValue("@nome", insumos.Nome);
                        query.Parameters.AddWithValue("@peso", insumos.Peso);
                        query.Parameters.AddWithValue("@valor_gasto", insumos.Valor_Gasto);                      
                        query.Parameters.AddWithValue("@estoque_medio", insumos.Estoque_medio);
                        query.Parameters.AddWithValue("@estoque_maximo", insumos.Estoque_maximo);
                        query.Parameters.AddWithValue("@id_for", fornecedorid.Id);

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
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erro 3007 : Contate o suporte!");
            }

        }
        public void Delete(Insumos insumos)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "DELETE FROM Insumos WHERE (id_ins = @id)";

                    query.Parameters.AddWithValue("@id", insumos.Id);

                    var result = query.ExecuteNonQuery();

                    if (result == 0)
                    {
                        MessageBox.Show("Erro ao remover o insumos. Verifique e tente novamente.");
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
    

