using System;
using System.Collections.Generic;
using System.Windows;
using NovoTayUmDoce.Conexão;
using NovoTayUmDoce.Helpers;
using NovoTayUmDoce.Componentes;
using MySql.Data.MySqlClient;
using System.Windows.Markup;

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
                            pedido.Total = DAOHelper.GetDouble(reader, "total_ped");
                            pedido.Desconto = DAOHelper.GetString(reader, "desconto_ped");
                            pedido.Produtos = DAOHelper.GetString(reader, "produtos_pd");
                            pedido.Data = (DateTime)DAOHelper.GetDateTime(reader, "data_ped");
                            pedido.Quantidade = DAOHelper.GetInt(reader, "quantidade_ped");
                            pedido.FormaPagamento = DAOHelper.GetString(reader, "forma_pagamento_ped");
                            pedido.Status = DAOHelper.GetString(reader, "status_ped");
                            pedido.Delivery = DAOHelper.GetString(reader, "delivery_ped");
                            pedido.Funcionario = new FuncionarioDAO().GetById(DAOHelper.GetInt(reader, "id_fun_fk"));
                            pedido.Cliente = new ClienteDAO().GetById(DAOHelper.GetInt(reader, "id_cli_fk"));
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
                    query.CommandText = "SELECT * FROM produto LEFT JOIN pedido ON id_ped = id_ped_fk";
                    using (var reader = query.ExecuteReader())
                    {
                        var lista = new List<Pedido>();

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

                            var endereco = new Endereco()
                            {
                                Id = DAOHelper.GetInt(reader, "id_end"),
                                Bairro = DAOHelper.GetString(reader, "bairro_end"),
                                Cidade = DAOHelper.GetString(reader, "cidade_end"),
                                Rua = DAOHelper.GetString(reader, "rua_end"),
                                Complemento = DAOHelper.GetString(reader, "complemento_end"),
                                Numero = DAOHelper.GetInt(reader, "numero_end"),
                                Cep = DAOHelper.GetString(reader, "cep_end")
                            };

                            var funcionario = new Funcionario()
                            {
                                Id = DAOHelper.GetInt(reader, "id_fun"),
                                Nome = DAOHelper.GetString(reader, "nome_fun"),
                                Data = DAOHelper.GetDateTime(reader, "data_nascimento_fun"),
                                Cpf = DAOHelper.GetString(reader, "cpf_fun"),
                                Contato = DAOHelper.GetString(reader, "contato_fun"),
                                Funcao = DAOHelper.GetString(reader, "funcao_fun"),
                                Email = DAOHelper.GetString(reader, "email_fun"),
                                Salario = DAOHelper.GetString(reader, "salario_fun"),
                                Endereco = endereco,
                            };
                            var cliente = new Cliente()
                            {
                                Id = DAOHelper.GetInt(reader, "id_cli"),
                                Nome = DAOHelper.GetString(reader, "nome_cli"),
                                Cpf = DAOHelper.GetString(reader, "cpf_cli"),
                                Contato = DAOHelper.GetString(reader, "contato_cli"),
                                DataNasc = DAOHelper.GetDateTime(reader, "data_nascimento_cli"),
                                Endereco = endereco,
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
                var query = conn.Query();
                query.CommandText = "INSERT INTO Pedido (total_ped, desconto_ped,produtos_ped, data_ped, quantidade_ped, forma_Pagamento, status_ped, delivre_ped, id_fun_fk, id_cli_fk) VALUES (@total, @desconto, @produtos, @data_ped, @quantidade, @forma_Pagamento, @status, @delivery, @id_fun, @id_cli)";
                query.CommandText = "INSERT INTO Pedido " +
                    "(total_ped, " +
                    "desconto_ped, " +
                    "produtos_ped, " +
                    "data_ped, " +
                    "quantidade_ped, " +
                    "forma_pagamento_ped, " +
                    "status_ped, " +
                    "delivery_ped, " +
                    "id_fun_fk, " +
                    "id_cli_fk) " +
                    "VALUES " +
                    "(@total, " +
                    "@desconto, " +
                    "@produtos, " +
                    "@data_ped, " +
                    "@quantidade, " +
                    "@forma_Pagamento, " +
                    "@status, " +
                    "@delivery, " +
                    "@id_fun, " +
                    "@id_cli)";

                query.Parameters.AddWithValue("@total", pedido.Total);
                query.Parameters.AddWithValue("@desconto", pedido.Desconto);
                query.Parameters.AddWithValue("@produtos", pedido.Produtos);
                query.Parameters.AddWithValue("@data_ped", pedido.Data.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@quantidade", pedido.Quantidade);
                query.Parameters.AddWithValue("@forma_Pagamento", pedido.FormaPagamento);
                query.Parameters.AddWithValue("@status", pedido.Status);
                query.Parameters.AddWithValue("@delivery", pedido.Delivery);
                query.Parameters.AddWithValue("@id_fun", pedido.Funcionario.Id);
                query.Parameters.AddWithValue("@id_cli", pedido.Cliente.Id);

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
    }
}
