using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TayUmDoceProjeto.Conexão;
using NovoTayUmDoce.Janelas;
using System.Windows;
using TayUmDoceProjeto.Models;
using MySqlX.XDevAPI;
using NovoTayUmDoce.Models;
using MySql.Data.MySqlClient;
using NovoTayUmDoce.Helpers;


namespace NovoTayUmDoce.Models
{
    internal class PedidoDAO
    {

        private static Conexao conn;


        public PedidoDAO()
        {
            conn = new Conexao();
        }
        public List<Pedido> List()
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "SELECT * FROM pedido LEFT JOIN endereco ON id_fun = id_fun_fk";
                var reader = query.ExecuteReader();

                var lista = new List<Pedido>();

                while (reader.Read())
                {

                    var cliente = new Cliente()
                    {
                        Id=reader.GetInt32("id_cli"),
                    };

                    var funcionario = new Funcionario()
                    {
                        Id = reader.GetInt32("id_fun"),
                       
                    };

                    var pedido = new Pedido()
                    {
                        Id = reader.GetInt32("id_ped"),
                        Total = reader.GetDouble("total_ped"),
                        Desconto = reader.GetString("desconto_ped"),
                        Produtos = reader.GetString("produtos_ped"),
                        Data = reader.GetDateTime("data_ped"),
                        Quantidade = reader.GetInt32("quantidade_ped"),
                        FormaPagamento = reader.GetString("forma_pagamento_ped"),
                        Status= reader.GetString("status_ped"),
                        Delivery=reader.GetString("Delivery_ped"),
                        Funcionario= funcionario,
                        Cliente= cliente,
                    };

                    lista.Add(pedido);
                }

                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Insert(Pedido pedido)
        {
            try
            {
                
                var funcionarioId = new FuncionarioDAO().Insert(pedido.Funcionario);
                var clienteId = new ClienteDAO().Insert(pedido.Cliente);

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Pedido (total_ped, desconto_ped, produtos_ped, data_ped, quantidade_ped, forma_Pagamento_ped, status_ped, delivery_ped, id_fun_fk, id_cli_fk) " +
                    $"VALUES (@data, @quantidade, @valor, @forma_Pagamento, @tipo_doce, @id_fun, @id_cli)";

                query.Parameters.AddWithValue("@total", pedido.Total);
                query.Parameters.AddWithValue("@desconto", pedido.Desconto);
                query.Parameters.AddWithValue("@produtos", pedido.Produtos);
                query.Parameters.AddWithValue("@data_ped", pedido.Data.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@quantidade", pedido.Quantidade);
                query.Parameters.AddWithValue("@forma_Pagamento",pedido.FormaPagamento);
                query.Parameters.AddWithValue("@status",pedido.Status);
                query.Parameters.AddWithValue("@delivery",pedido.Delivery);

                query.Parameters.AddWithValue("@id_fun", funcionarioId);
                query.Parameters.AddWithValue("@id_cli", clienteId);


                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erro 3007cliente : Contate o suporte!");
            }
        }

        /* METODO LIST PARA LISTAR*/

        /*public List<Funcionario> List()
        {
            try
            {
                List<Funcionario> listaFuncionario = new List<Funcionario>();

                var query = conexao.Query();
                query.CommandText = "select * from Funcionario";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum Funcionario foi encotrado!");
                }

                while (reader.Read())
                {
                    listaFuncionario.Add(new Funcionario()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "func_id"),
                        Nome = AuxiliarDAO.GetString(reader, "func_nome"),
                        Sexo = AuxiliarDAO.GetString(reader, "func_sexo"),
                        Nascimento = AuxiliarDAO.GetDateTime(reader, "func_nascimento"),
                        RG = AuxiliarDAO.GetString(reader, "func_rg"),
                        CPF = AuxiliarDAO.GetString(reader, "func_cpf"),
                        Email = AuxiliarDAO.GetString(reader, "func_email"),
                        Contato = AuxiliarDAO.GetString(reader, "func_contato"),
                        Funcao = AuxiliarDAO.GetString(reader, "func_funcao"),
                        Salario = AuxiliarDAO.GetFloat(reader, "func_salario"),
                        Endereco = new EnderecoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_ende_id"))
                    });
                }

                return listaFuncionario;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexao.Close();
            }
        }*/
    }
}
