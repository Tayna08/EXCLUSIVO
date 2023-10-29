 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using NovoTayUmDoce.Conexão;
using NovoTayUmDoce.Helpers;

namespace NovoTayUmDoce.Models
{
    internal class CompraDAO
    {
        private static Conexao conn;

        public CompraDAO()
        {
            conn = new Conexao();
        }

        public Compra GetById(int id)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM Compra WHERE (id_com = @id)";
                    query.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = query.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Nenhuma compra foi encontrada!");
                            return null;
                        }

                        var compra = new Compra();

                        while (reader.Read())
                        {
                            compra.Id = DAOHelper.GetInt(reader, "id_com");
                            compra.Valor = DAOHelper.GetDouble(reader, "valor_com");
                            compra.Nome = DAOHelper.GetString(reader, "nome_com");
                            compra.Data = (DateTime)DAOHelper.GetDateTime(reader, "data_com");
                            compra.Quantidade = DAOHelper.GetInt(reader, "quantidade_com");
                            compra.Descricao = DAOHelper.GetString(reader, "descricao_com");
                            compra.Funcionario = new FuncionarioDAO().GetById(DAOHelper.GetInt(reader, "id_fun_fk"));
                            compra.Despesa = new DespesaDAO().GetById(DAOHelper.GetInt(reader, "id_des_fk"));
                            compra.Fornecedor = new FornecedorDAO().GetById(DAOHelper.GetInt(reader, "id_for_fk"));
                        }

                        return compra;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public List<Compra> List()
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM compra LEFT JOIN funcionario ON id_fun = id_fun_fk";
                    query.CommandText = "SELECT * FROM compra LEFT JOIN despesa ON id_des = id_des_fk";
                    query.CommandText = "SELECT * FROM compra LEFT JOIN fornecedor ON id_for = id_for_fk";
                    using (var reader = query.ExecuteReader())
                    {
                        var lista = new List<Compra>();

                        while (reader.Read())
                        {

                            var compra = new Compra()
                            {

                                Id = DAOHelper.GetInt(reader, "id_com"),
                                Valor = DAOHelper.GetDouble(reader, "valor_com"),
                                Nome = DAOHelper.GetString(reader, "nome_com"),
                                Data = (DateTime)DAOHelper.GetDateTime(reader, "data_com"),
                                Quantidade = DAOHelper.GetInt(reader, "quantidade_com"),
                                Descricao = DAOHelper.GetString(reader, "descricao_com"),

                            };

                            lista.Add(compra);
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

        public void Insert(Compra compra)
        {

            try
            {
                var funcionarioId = new FuncionarioDAO().GetById(compra.Funcionario.Id);
                var despesaId = new DespesaDAO().GetById(compra.Despesa.Id);
                var FornecedorId = new FornecedorDAO().GetById(compra.Fornecedor.Id);

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Compra (valor_com, nome_com, data_com, quantidade_com, descricao_com, id_fun_fk, id_des_fk, id_for_fk)" +
                                    $"VALUES (@Valor, @Nome, @Data, @Quantidade, @Descricao, @id_fun, @id_des, @id_for)";

                query.Parameters.AddWithValue("@Valor", compra.Valor);
                query.Parameters.AddWithValue("@Nome", compra.Nome);
                query.Parameters.AddWithValue("@Data", compra.Data.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@Quantidade", compra.Quantidade);
                query.Parameters.AddWithValue("@Descricao", compra.Descricao);
                query.Parameters.AddWithValue("@id_fun", funcionarioId.Id);
                query.Parameters.AddWithValue("@id_des", despesaId.Id);
                query.Parameters.AddWithValue("@id_for", FornecedorId.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                MessageBox.Show("Error 3007: Contate o suporte!");

            }
            finally
            {

                conn.Close();

            }

        }
        public void Delete(Compra compra)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "DELETE FROM compra WHERE (id_com= @id)";

                    query.Parameters.AddWithValue("@id", compra.Id);

                    var result = query.ExecuteNonQuery();

                    if (result == 0)
                    {
                        MessageBox.Show("Erro ao remover a compra. Verifique e tente novamente.");
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
