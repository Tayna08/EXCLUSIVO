
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;
using NovoTayUmDoce.Conexão;
using NovoTayUmDoce.Helpers;
using System.Windows.Media.Media3D;

namespace NovoTayUmDoce.Models
{
    class VendaDAO
    {
        private static Conexao conn;

        public VendaDAO()
        {
            conn = new Conexao();
        }

        public Venda GetById(int id)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM Venda WHERE (id_ven = @id)";
                    query.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = query.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Nenhuma venda foi encontrada!");
                            return null;
                        }

                        var venda = new Venda();

                        while (reader.Read())
                        {
                            venda.Id = DAOHelper.GetInt(reader, "id_ven");
                            venda.Valor = DAOHelper.GetDouble(reader, "valor_ven");
                            venda.Produto = DAOHelper.GetString(reader, "produtos_ven");
                            venda.Data = (DateTime)DAOHelper.GetDateTime(reader, "data_ven");
                            venda.Quantidade = DAOHelper.GetString(reader, "quantidade_produto_ven");
                            venda.Forma_pagamento = DAOHelper.GetString(reader, "forma_pagamento_ven");
                            venda.Funcionario = new FuncionarioDAO().GetById(DAOHelper.GetInt(reader, "id_fun_fk"));
                            venda.Cliente = new ClienteDAO().GetById(DAOHelper.GetInt(reader, "id_cli_fk"));
                            //venda.Caixa = new CaixaDAO().GetById(DAOHelper.GetInt(reader, "id_cai_fk"));
                        }

                        return venda;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public List<Venda> List()
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM venda LEFT JOIN funcionario ON id_fun = id_fun_fk";
                    query.CommandText = "SELECT * FROM venda LEFT JOIN cliente ON id_des = id_cli_fk";
                    query.CommandText = "SELECT * FROM venda LEFT JOIN caixa ON id_cai = id_cai_fk";
                    using (var reader = query.ExecuteReader())
                    {
                        var lista = new List<Venda>();

                        while (reader.Read())
                        {

                            var venda = new Venda()
                            {

                                Id = DAOHelper.GetInt(reader, "id_ven"),
                                Valor = DAOHelper.GetDouble(reader, "valor_ven"),
                                Desconto = DAOHelper.GetString(reader, "desconto_ven"),
                                Data = (DateTime)DAOHelper.GetDateTime(reader, "data_ven"),
                                Quantidade = DAOHelper.GetString(reader, "quantidade_ven"),
                                Produto = DAOHelper.GetString(reader, "produtos_ven"),
                                Forma_pagamento = DAOHelper.GetString(reader, "forma_pagamento_ven"),

                            };

                            lista.Add(venda);
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

        public void Insert(Venda venda)
        {
            try
            {

                var funcionarioId = new FuncionarioDAO().GetById(venda.Funcionario.Id);
                var clienteId = new ClienteDAO().GetById(venda.Cliente.Id);
                //var caixaId = new CaixaDAO().GetById(venda.Caixa.Id);

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Venda(valor_ven, produtos_ven, forma_pagamento_ven, data_ven, quantidade_produto_ven, desconto_ven, id_cli_fk, id_fun_fk, id_cai_fk) " +
                    $"VALUES (@Valor, @Produto, @Forma_pagamento, @Data, @Quantidade, @Desconto, @id_cli, @id_fun, @id_cai)";

                query.Parameters.AddWithValue("@Valor", venda.Valor);
                query.Parameters.AddWithValue("@Produto", venda.Produto);
                query.Parameters.AddWithValue("@Forma_pagamento", venda.Forma_pagamento);
                query.Parameters.AddWithValue("@Data", venda.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@Quantidade", venda.Quantidade);
                query.Parameters.AddWithValue("@Desconto", venda.Desconto);
                query.Parameters.AddWithValue("@id_cli", venda.Cliente);
                query.Parameters.AddWithValue("@id_fun", venda.Funcionario);
               // query.Parameters.AddWithValue("@id_cai", venda.Caixa);

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
            finally
            {
                conn.Close();
            }
        }

        public void Delete(Venda venda)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "DELETE FROM venda WHERE (id_ven = @id)";

                    query.Parameters.AddWithValue("@id", venda.Id);

                    var result = query.ExecuteNonQuery();

                    if (result == 0)
                    {
                        MessageBox.Show("Erro ao remover a venda. Verifique e tente novamente.");
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
