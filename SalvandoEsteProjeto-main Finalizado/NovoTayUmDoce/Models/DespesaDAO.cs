using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NovoTayUmDoce.Models;
using NovoTayUmDoce.Conexão;
using MySql.Data.MySqlClient;
using NovoTayUmDoce.Helpers;

namespace NovoTayUmDoce.Models
{
    internal class DespesaDAO
    {
        private static Conexao conn;

        public DespesaDAO()
        {
            conn = new Conexao();
        }

        public Despesa GetById(int id)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM Despesa WHERE (id_des = @id)";
                    query.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = query.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Nenhuma despesa foi encontrada!");
                            return null;
                        }

                        var despesa = new Despesa();

                        while (reader.Read())
                        {
                            despesa.Id = reader.GetInt32("id_des");
                            despesa.NomeDespesa = reader.GetString("nome_des");
                            despesa.Descricao = reader.GetString("descricao_des");
                            despesa.FormaPagamento = reader.GetString("forma_pag_des");
                            despesa.Data = reader.GetDateTime("data_des");
                            despesa.Hora = reader.GetString("hora_des");
                            despesa.Valor = reader.GetDouble("valor_des");
                            despesa.Vencimento = reader.GetDateTime("vencimento_des");

                        }

                        return despesa;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public List<Despesa> List()
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM Despesa";
                    using (var reader = query.ExecuteReader())
                    {
                        var lista = new List<Despesa>();

                        while (reader.Read())
                        {
                            var despesa = new Despesa();
                            {

                                despesa.Id = DAOHelper.GetInt(reader, "id_des");
                                despesa.NomeDespesa = DAOHelper.GetString(reader, "nome_des");
                                despesa.Descricao = DAOHelper.GetString(reader, "descricao_des");
                                despesa.FormaPagamento = DAOHelper.GetString(reader, "forma_pag_des");
                                despesa.Data = DAOHelper.GetDateTime(reader, "data_des");
                                despesa.Hora = DAOHelper.GetString(reader, "hora_des");
                                despesa.Valor = DAOHelper.GetDouble(reader, "valor_des");
                                despesa.Vencimento = DAOHelper.GetDateTime(reader, "vencimento_des");

                            }

                            lista.Add(despesa);
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

        public string Insert(Despesa despesa)
        {
            try
            {

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Despesa (nome_des, descricao_des, forma_pag_des, data_des, hora_des, valor_des, vencimento_des) " +
                    $"VALUES (@NomeDespesa, @Descricao, @FormaPagamento, @Data, @Hora, @Valor, @Vencimento)";

                query.Parameters.AddWithValue("@NomeDespesa", despesa.NomeDespesa);
                query.Parameters.AddWithValue("@Descricao", despesa.Descricao);
                query.Parameters.AddWithValue("@FormaPagamento", despesa.FormaPagamento);
                query.Parameters.AddWithValue("@Data", despesa.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@Hora", despesa.Hora);
                query.Parameters.AddWithValue("@Valor", despesa.Valor);
                query.Parameters.AddWithValue("@Vencimento", despesa.Vencimento?.ToString("yyyy-MM-dd"));


                var resultado = (string)query.ExecuteScalar();
                return resultado;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inserir despesas: {ex.Message}");
                throw;
            }
        }

        public void Delete(Despesa despesa)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "DELETE FROM despesa WHERE (id_des = @id)";

                    query.Parameters.AddWithValue("@id", despesa.Id);

                    var result = query.ExecuteNonQuery();

                    if (result == 0)
                    {
                        MessageBox.Show("Erro ao remover a depesa. Verifique e tente novamente.");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public void Update(Despesa despesa)
        {
            try
            {

                var query = conn.Query();
                query.CommandText = "UPDATE Despesa SET nome_des = @nome, descricao_des = @descricao, forma_pag_des = @forma_pag, " +
                                     "data_des = @data, hora_des = @hora, valor_des = @valor, vencimento_des = @vencimento WHERE id_des = @id";

                query.Parameters.AddWithValue("@nome", despesa.NomeDespesa);
                query.Parameters.AddWithValue("@descricao", despesa.Descricao);
                query.Parameters.AddWithValue("@forma_pag", despesa.FormaPagamento);
                query.Parameters.AddWithValue("@data", despesa.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@hora", despesa.Hora);
                query.Parameters.AddWithValue("@valor", despesa.Valor);
                query.Parameters.AddWithValue("@vencimento", despesa.Vencimento?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@id", despesa.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Atualização do registro não foi realizada.");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Erro ao atualizar a despesa: {e.Message}");
                throw;
            }


        }
    }
}

