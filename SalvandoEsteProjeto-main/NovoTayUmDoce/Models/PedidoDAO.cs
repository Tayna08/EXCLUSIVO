using System;
using System.Collections.Generic;
using System.Windows;
using NovoTayUmDoce.Conexão;
using NovoTayUmDoce.Helpers;
using MySql.Data.MySqlClient;
using System.Windows.Markup;
using NovoTayUmDoce.Componentes;

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
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM Pedido WHERE (id_ped = @id)";
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
                            pedido.Total = DAOHelper.GetString(reader, "total_ped");
                            pedido.Data = (DateTime)DAOHelper.GetDateTime(reader, "data_ped");
                            pedido.Valor = DAOHelper.GetString(reader, "valor_ped");
                            pedido.Quant = DAOHelper.GetString(reader, "Quant");
                            pedido.FormaPagamento = DAOHelper.GetString(reader, "forma_pagamento_ped");
                            pedido.Status = DAOHelper.GetString(reader, "status_ped");
                            pedido.Funcionario = new FuncionarioDAO().GetById(DAOHelper.GetInt(reader, "id_fun_fk"));
                            pedido.Cliente = new ClienteDAO().GetById(DAOHelper.GetInt(reader, "id_cli_fk"));
                            pedido.Produto = new ProdutoDAO().GetById(DAOHelper.GetInt(reader, "id_pro_fk"));

                        }

                        return pedido;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
        public List<Pedido> List()
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM pedido LEFT JOIN funcionario ON id_fun = id_fun_fk";
                    query.CommandText = "SELECT * FROM pedido LEFT JOIN cliente ON id_cli = id_cli_fk";
                    query.CommandText = "SELECT * FROM produto LEFT JOIN produto ON id_pro = id_pro_fk";

                    using (var reader = query.ExecuteReader())
                    {
                        var lista = new List<Pedido>();

                        while (reader.Read())
                        {

                            var pedido = new Pedido()
                            {

                                Id = DAOHelper.GetInt(reader, "id_ped"),
                                FormaPagamento = DAOHelper.GetString(reader, "forma_pagamento_ped"),
                                Status = DAOHelper.GetString(reader, "status_ped"),
                               
                            };
                            
                            lista.Add(pedido);
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
        public void Insert(Pedido pedido)
        {

            try
            {
              
                var funcionarioId = new FuncionarioDAO().GetById(pedido.Funcionario.Id);
                var clienteId = new ClienteDAO().GetById(pedido.Cliente.Id);
                var produtoId = new ProdutoDAO().GetById(pedido.Produto.Id);

                
                if(funcionarioId.Id >0)
                {
                    using (var query= conn.Query())
                    {
                        query.CommandText = "INSERT INTO Pedido (total_ped,  data_ped, quant_ped hora_ped, valor_ped, forma_Pagamento_ped, status_ped, id_fun_fk, id_cli_fk, id_pro_fk) " +
                            "VALUES (@total, @data_ped, @quant_ped, @hora_ped, @forma_Pagamento, @status, @id_fun, @id_cli, @id_pro)";


                        query.Parameters.AddWithValue("@total", pedido.Total);
                        query.Parameters.AddWithValue("@data_ped", pedido.Data.ToString("yyyy-MM-dd"));
                        query.Parameters.AddWithValue("@Quant", pedido.Quant);
                        query.Parameters.AddWithValue("@hora_ped", pedido.Hora);
                        query.Parameters.AddWithValue("@valor_ped", pedido.Valor);
                        query.Parameters.AddWithValue("@forma_Pagamento", pedido.FormaPagamento);
                        query.Parameters.AddWithValue("@status", pedido.Status);
                        query.Parameters.AddWithValue("@id_fun", funcionarioId.Id);
                        query.Parameters.AddWithValue("@id_cli", clienteId.Id);
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
               
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir os dados: " + ex.Message);
            }
        }

        private void InsertItens(long compraId, List<PedidoItem> itens)
        {

            foreach (PedidoItem item in itens)
            {
                var query = conn.Query();
                query.CommandText = "INSERT INTO itens_compra (cod_comp_fk, cod_prod_fk, quantidade_itenc, valor_itenc, valor_total_itenc) " +
                    "VALUES (@compra, @produto, @quantidade, @valor, @valor_total)";

                query.Parameters.AddWithValue("@compra", compraId);
                query.Parameters.AddWithValue("@produto", item.Produto.Id);
               // query.Parameters.AddWithValue("@quantidade", item.Quantidade);
                query.Parameters.AddWithValue("@valor", item.Valor);
               // query.Parameters.AddWithValue("@valor_total", item.ValorTotal);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("Os itens da compra não foi adicionada. Verifique e tente novamente.");
            }
        }
        public void Delete(Pedido pedido)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "DELETE FROM pedido WHERE (id_ped = @id)";

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
    }
}
