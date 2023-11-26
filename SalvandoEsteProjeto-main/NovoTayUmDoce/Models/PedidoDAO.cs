﻿using System;
using System.Collections.Generic;
using System.Windows;
using NovoTayUmDoce.Conexão;
using NovoTayUmDoce.Helpers;
using MySql.Data.MySqlClient;
using System.Windows.Markup;
using NovoTayUmDoce.Componentes;
using NPOI.SS.Formula.Functions;
using System.Collections;

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
                            pedido.Data = (DateTime)DAOHelper.GetDateTime(reader, "data_ped");
                            pedido.Quant = DAOHelper.GetString(reader, "quant_ped");
                            pedido.Valor = DAOHelper.GetString(reader, "valor_ped");
                            pedido.Total = DAOHelper.GetString(reader, "total_ped");
                            pedido.FormaPagamento = DAOHelper.GetString(reader, "forma_pagamento_ped");
                            pedido.Status = DAOHelper.GetString(reader, "status_ped");
                            pedido.Funcionario = new FuncionarioDAO().GetById(DAOHelper.GetInt(reader, "id_fun_fk"));
                            pedido.Cliente = new ClienteDAO().GetById(DAOHelper.GetInt(reader, "id_cli_fk"));
                            pedido.Produto= new ProdutoDAO().GetById(DAOHelper.GetInt(reader, "id_pro_fk"));

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
       
        public void Insert(Pedido pedido)
        {

            try
            {
                if (pedido.Produto != null)
                {

                    var funcionarioId = new FuncionarioDAO().GetById(pedido.Funcionario.Id);
                    var clienteId = new ClienteDAO().GetById(pedido.Cliente.Id);
                    var produtoId = new ProdutoDAO().GetById(pedido.Produto.Id);


                    if (funcionarioId.Id > 0)
                    {
                        using (var query = conn.Query())
                        {
                            query.CommandText = "INSERT INTO Pedido (data_ped, hora_ped,quant_ped,valor_ped,total_ped, forma_Pagamento_ped, status_ped, id_fun_fk, id_cli_fk, id_pro_fk) " +
                                "VALUES (@data, @hora,@quant, @valor,@total, @forma_Pagamento, @status, @id_fun, @id_cli,@id_pro)";



                            query.Parameters.AddWithValue("@data_ped", pedido.Data.ToString("yyyy-MM-dd"));
                            query.Parameters.AddWithValue("@hora_ped", pedido.Hora);
                            query.Parameters.AddWithValue("@quant_ped", pedido.Quant);
                            query.Parameters.AddWithValue("@valor_ped", pedido.Valor);
                            query.Parameters.AddWithValue("@total_ped", pedido.Total);
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
               
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir os dados: " + ex.Message);
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

        public List<Pedido> List()
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM pedido " +
                                        "LEFT JOIN funcionario ON id_fun = id_fun_fk " +
                                        "LEFT JOIN cliente ON id_cli = id_cli_fk " +
                                        "LEFT JOIN produto ON id_pro = id_pro_fk";

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
    
    }
}
