﻿using MySql.Data.MySqlClient;
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
        public Produto GetById(int id)
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

                        var insumo = new Insumos();

                        while (reader.Read())
                        {
                            insumo.Id = reader.GetInt32("id_ins");
                            insumo.Nome = reader.GetString("nome_ins");
                            insumo.Peso = reader.GetString("peso_ins");
                            insumo.Valor_Gasto = reader.GetDouble("valor_gasto_ins");
                            insumo.Estoque_medio = reader.GetString("estoque_medio_ins");
                            insumo.Estoque_maximo = reader.GetString("estoque_maximo_ins");
                           

                        }

                        return insumo;
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
                    query.CommandText = "SELECT * FROM produto LEFT JOIN pedido ON id_ped = id_ped_fk";
                    using (var reader = query.ExecuteReader())
                    {
                        var lista = new List<Produto>();

                        while (reader.Read())
                        {
                            var pedido = new Pedido()
                            {

                                Id = DAOHelper.GetInt(reader, "id_ped"),
                                Total = DAOHelper.GetDouble(reader, "total_ped"),
                                Desconto = DAOHelper.GetString(reader, "desconto_ped"),
                                Produtos = DAOHelper.GetString(reader, "produtos_ped"),
                                Data = (DateTime)DAOHelper.GetDateTime(reader, "data_ped"),
                                Quantidade = DAOHelper.GetInt(reader, "quantidade_ped"),
                                FormaPagamento = DAOHelper.GetString(reader, "forma_pagamento_ped"),
                                Status = DAOHelper.GetString(reader, "status_ped"),
                                Delivery = DAOHelper.GetString(reader, "delivery_ped"),

                            };
                            var produto = new Produto()
                            {
                                Id = DAOHelper.GetInt(reader, "id_pro"),
                            };

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
                var pedidoId = new PedidoDAO().GetById(produto.Pedido.Id);

                if (pedidoId.Id > 0)
                {
                    using (var query = conn.Query())
                    {

                        query.CommandText = $"INSERT INTO Produto (nome_pro, peso_pro, valor_gasto_pro, valor_venda_pro, data_fabricacao_pro, hora_pro, estoque_medio, estoque_maximo, quantidade_pro, tipo_pro, descricao_pro, id_ped_fk ) " +
                            $"VALUES (@nome, @peso, @valor_gasto, @valor_venda, @data_fabricacao, @hora, @estoque_medio, @estoque_maximo, @quantidade, @tipo, @descricao, @id_ped)";

                        query.Parameters.AddWithValue("@nome", produto.Nome);
                        query.Parameters.AddWithValue("@peso", produto.Peso);
                        query.Parameters.AddWithValue("@valor_gasto", produto.Valor_Gasto);
                        query.Parameters.AddWithValue("@valor_venda", produto.Valor_Venda);
                        query.Parameters.AddWithValue("@data_fabricacao", produto.Data?.ToString("yyyy-MM-dd"));
                        query.Parameters.AddWithValue("@hora", produto.Hora);
                        query.Parameters.AddWithValue("@estoque_medio", produto.Estoque_medio);
                        query.Parameters.AddWithValue("@estoque_maximo", produto.Estoque_maximo);
                        query.Parameters.AddWithValue("@quantidade", produto.Quantidade);
                        query.Parameters.AddWithValue("@tipo", produto.Tipo);
                        query.Parameters.AddWithValue("@descricao", produto.Descricao);
                        query.Parameters.AddWithValue("@id_ped", pedidoId.Id);

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
            finally
            {
                conn.Close();
            }
        }
    }
}
    }
}