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
                            despesa.Descricao = reader.GetString("peso_des");
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
                                despesa.Descricao = DAOHelper.GetString(reader, "peso_des");
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

        public void Insert(Despesa despesa)
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


    }
}

