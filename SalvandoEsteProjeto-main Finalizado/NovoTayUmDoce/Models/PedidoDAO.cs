using System;
using System.Collections.Generic;
using System.Windows;
using NovoTayUmDoce.Conexão;
using NovoTayUmDoce.Helpers;
using MySql.Data.MySqlClient;
using System.Windows.Markup;
using NovoTayUmDoce.Componentes;
using NPOI.SS.Formula.Functions;
using System.Collections;
using NovoTayUmDoce.Conexão;
using MySqlX.XDevAPI;

namespace NovoTayUmDoce.Models
{
    internal class PedidoDAO
    {
        private static Conexao conn;

        public PedidoDAO()
        {
            conn = new Conexao();
        }

        public Pedido GetById(int id)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "SELECT * FROM Pedido WHERE id_ped = @id";
                query.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = query.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("Nenhum pedido foi encontrado!");
                        return null;
                    }

                    var pedido = new Pedido();

                    while (reader.Read())
                    {
                        pedido.Id = DAOHelper.GetInt(reader, "id_ped");
                        pedido.Data = (DateTime)DAOHelper.GetDateTime(reader, "data_ped");
                        pedido.Hora = DAOHelper.GetString(reader, "hora_ped");
                        pedido.FormaRecebimento = DAOHelper.GetString(reader, "forma_recebimento_ped");
                        pedido.Quantidade = DAOHelper.GetInt(reader, "quantidade_ped");
                        pedido.Total = DAOHelper.GetDouble(reader, "total_ped");
                        pedido.Status = DAOHelper.GetString(reader, "status_ped");
                        pedido.Funcionario = new FuncionarioDAO().GetById(DAOHelper.GetInt(reader, "id_fun_fk"));
                        pedido.Cliente = new ClienteDAO().GetById(DAOHelper.GetInt(reader, "id_cli_fk"));
                        pedido.Produto = new ProdutoDAO().GetById(DAOHelper.GetInt(reader, "id_pro_fk"));
                    }

                    return pedido;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string Insert(Pedido pedido)
        {
            try
            {
                var query = conn.Query();
                query.CommandText =
                    "INSERT INTO Pedido " +
                    "(data_ped, hora_ped, forma_recebimento_ped, quantidade_ped, total_ped, status_ped, id_fun_fk, id_cli_fk, id_pro_fk) " +
                    "VALUES " +
                    "(@data_ped, @hora_ped, @forma_recebimento_ped, @quantidade_ped, @total_ped, @status_ped, @id_fun_fk, @id_cli_fk, @id_pro_fk)";

                query.Parameters.AddWithValue("@data_ped", pedido.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@hora_ped", pedido.Hora.ToString());
                query.Parameters.AddWithValue("@forma_recebimento_ped", pedido.FormaRecebimento);
                query.Parameters.AddWithValue("@quantidade_ped", pedido.Quantidade);
                query.Parameters.AddWithValue("@total_ped", pedido.Total);
                query.Parameters.AddWithValue("@status_ped", pedido.Status);
                query.Parameters.AddWithValue("@id_fun_fk", pedido.Funcionario.Id);
                query.Parameters.AddWithValue("@id_cli_fk", pedido.Cliente.Id);
                query.Parameters.AddWithValue("@id_pro_fk", pedido.Produto.Id);

                query.ExecuteNonQuery();

                return "Inserção bem-sucedida";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir os dados: " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Delete(Pedido pedido)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "DELETE FROM pedido WHERE id_ped = @id";
                    query.Parameters.AddWithValue("@id", pedido.Id);

                    var result = query.ExecuteNonQuery();

                    if (result == 0)
                    {
                        MessageBox.Show("Erro ao remover o pedido. Verifique e tente novamente.");
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

        public List<Pedido> List()
        {
            try
            {
                List<Pedido> pedidos = new List<Pedido>();

                var query = conn.Query();
                query.CommandText = "select * from Pedido";

                using (MySqlDataReader reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pedidos.Add(new Pedido()
                        {
                            Id = DAOHelper.GetInt(reader, "id_ped"),
                            Data = (DateTime)DAOHelper.GetDateTime(reader, "data_ped"),
                            Hora = DAOHelper.GetString(reader, "hora_ped"),
                            FormaRecebimento = DAOHelper.GetString(reader, "forma_recebimento_ped"),
                            Quantidade = DAOHelper.GetInt(reader, "quantidade_ped"),
                            Total = DAOHelper.GetDouble(reader, "total_ped"),
                            Status = DAOHelper.GetString(reader, "status_ped"),
                            Produto = new ProdutoDAO().GetById(DAOHelper.GetInt(reader, "id_pro_fk")),
                            Cliente = new ClienteDAO().GetById(DAOHelper.GetInt(reader, "id_cli_fk")),
                            Funcionario = new FuncionarioDAO().GetById(DAOHelper.GetInt(reader, "id_fun_fk"))
                        });
                    }
                }

                return pedidos;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(Pedido pedido)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE pedido set data_ped = @data, hora_ped = @hora, quantidade_ped = @quantidade, total_ped = @total, status_ped = @status WHERE id_ped = @id";
                query.Parameters.AddWithValue("@data", pedido.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@hora", pedido.Hora);
                query.Parameters.AddWithValue("@quantidade", pedido.Quantidade);
                query.Parameters.AddWithValue("@total", pedido.Total);
                query.Parameters.AddWithValue("@status", pedido.Status);


                query.Parameters.AddWithValue("@id", pedido.Id);
                var resultado = query.ExecuteNonQuery();
                if (resultado == 0)
                    throw new Exception("Atualização do registro não foi realizada.");

            }
            catch (Exception e)
            {
                MessageBox.Show($"Erro ao atualizar pedido: {e.Message}");

                throw;
            }
        }

    }
}
