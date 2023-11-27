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
                
                query.CommandText = "SELECT * FROM Pedido WHERE (id_ped = @id)";
                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();
                    
                if (!reader.HasRows)
                {
                   MessageBox.Show("Nenhum pedido foi encontrado!");
                   return null;
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
                            pedido.Quantidade = DAOHelper.GetString(reader, "quant_ped");
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
            catch (Exception e)
            {
                throw e;
            }
        }
       
        public long Insert(Pedido pedido)
        {
           

            try
            {
                var query = conn.Query();
                var funcionarioId = new FuncionarioDAO().GetById(pedido.Funcionario.Id);
                var clienteId = new ClienteDAO().GetById(pedido.Cliente.Id);
                var produtoId = new ProdutoDAO().GetById(pedido.Produto.Id);

                        
                query.CommandText = "INSERT INTO Pedido (data_ped, hora_ped, forma_recebimento_ped, quantidade_ped, total_ped, status_ped, id_fun_fk, id_cli_fk, id_pro_fk) " +
                "VALUES (@data, @hora, @forma_recebimento, @quantidade, @status, @id_fun, @id_cli, @id_pro)";
                
                query.Parameters.AddWithValue("@data", pedido.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@hora", pedido.Hora);
                query.Parameters.AddWithValue("@forma_recebimento", pedido.FormaRecebimento);
                query.Parameters.AddWithValue("@quantidade", pedido.Quantidade);
                query.Parameters.AddWithValue("@status", pedido.Status);
                query.Parameters.AddWithValue("@id_fun", funcionarioId.Id);
                query.Parameters.AddWithValue("@id_cli", clienteId.Id);
                query.Parameters.AddWithValue("id_pro", produtoId.Id);

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
                MessageBox.Show("Erro ao inserir os dados: " + ex.Message);
                return 0;
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
                    query.CommandText = "SELECT * FROM pedido LEFT JOIN funcionario ON id_fun = id_fun_fk";
                    query.CommandText = "SELECT * FROM pedido LEFT JOIN cliente ON id_cli = id_cli_fk";
                    query.CommandText = "SELECT * FROM pedido LEFT JOIN cliente ON id_pro = id_pro_fk";


                    using (var reader = query.ExecuteReader())
                    {
                        var lista = new List<Pedido>();

                        while (reader.Read())
                        {
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

                            var produto = new Produto()
                            {
                                Id = DAOHelper.GetInt(reader, "id_pro"),
                                Nome = DAOHelper.GetString(reader, "nome_pro"),
                                Descricao = DAOHelper.GetString(reader, "descricao_pro"),
                                Peso = DAOHelper.GetString(reader, "peso_pro"),
                                Tipo = DAOHelper.GetString(reader, "tipo_pro"),
                                Valor_Venda = DAOHelper.GetDouble(reader, "valor_venda_pro"),

                            };
                            var funcionario = new Funcionario()
                            {
                                Id = DAOHelper.GetInt(reader, "id_fun"),
                                Nome = DAOHelper.GetString(reader, "nome_fun"),
                                Data = (DateTime)DAOHelper.GetDateTime(reader, "data_nascimento_fun"),
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
                                DataNasc = (DateTime)DAOHelper.GetDateTime(reader, "data_nascimento_cli"),
                                Cpf = DAOHelper.GetString(reader, "cpf_cli"),
                                Contato = DAOHelper.GetString(reader, "contato_cli"),
                                Endereco = endereco,

                            };
                            var pedido = new Pedido()
                            {
                                Id = DAOHelper.GetInt(reader, "id_ped"),
                                Data = (DateTime)DAOHelper.GetDateTime(reader, "data_ped"),
                                Hora = DAOHelper.GetString(reader, "hora_ped"),
                                FormaRecebimento = DAOHelper.GetString(reader, "forma_recebimento_ped"),
                                Quantidade = DAOHelper.GetInt(reader, "quantidade_ped"),
                                Status = DAOHelper.GetString(reader, "status_ped"),
                                Produto = produto,
                                Cliente = cliente,
                                Funcionario = funcionario,

                            };

                            lista.Add(pedido);
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
    
    }
}
