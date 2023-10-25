using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovoTayUmDoce.Janelas;
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
                                Id = DAOHelper.GetInt(reader,"id_pro"),
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
                var query = conn.Query();
                query.CommandText = $"INSERT INTO Produto (nome_pro, valor_pro, peso_pro, descricao_pro, data_fabricacao_pro, quantidade_pro)" +
                    $"VALUES (@nome, @valor, @peso, @descricao, @data_fabricacao, @quantidade)";

                query.Parameters.AddWithValue("@nome", produto.Nome);
                //query.Parameters.AddWithValue("@valor", produto.Valor_Maximo);
                query.Parameters.AddWithValue("@peso", produto.Peso);
                query.Parameters.AddWithValue("@descricao", produto.Descricao);
                query.Parameters.AddWithValue("@data_fabricacao", produto.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@quantidade", produto.Quantidade);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erro 3007 : Contate o suporte!");
            }
          
        }
    }
}
