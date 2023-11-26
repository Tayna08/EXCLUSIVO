using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Windows;
using NovoTayUmDoce.Helpers;
using NovoTayUmDoce.Conexão;
using NPOI.SS.Formula.Functions;
using System.Windows.Input;

namespace NovoTayUmDoce.Models
{
    class ClienteDAO
    {
        private Conexao conn;

        public ClienteDAO()
        {
            conn = new Conexao();
        }

        public List<Cliente> List()
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM cliente LEFT JOIN endereco ON id_end = id_end_fk";
                    using (var reader = query.ExecuteReader())
                    {
                        var lista = new List<Cliente>();

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

                            var cliente = new Cliente()
                            {
                                Id = DAOHelper.GetInt(reader, "id_cli"),
                                Nome = DAOHelper.GetString(reader, "nome_cli"),
                                Cpf = DAOHelper.GetString(reader, "cpf_cli"),
                                Contato = DAOHelper.GetString(reader, "contato_cli"),
                                DataNasc = DAOHelper.GetDateTime(reader, "data_nascimento_cli"),
                                Endereco = endereco,
                            };

                            lista.Add(cliente);
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

        //insert
        public string Insert(Cliente cliente)
        {
            try
            {
                var enderecoId = new EnderecoDAO().Insert(cliente.Endereco);
                var query = conn.Query();
                query.CommandText = "CALL Campo_Cliente(@nome, @cpf, @data_nasc, @contato, @id_end)";

                query.Parameters.AddWithValue("@nome", cliente.Nome);
                query.Parameters.AddWithValue("@cpf", cliente.Cpf);
                query.Parameters.AddWithValue("@data_nasc", cliente.DataNasc?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@contato", cliente.Contato);
                query.Parameters.AddWithValue("@id_end", enderecoId);

                var resultado = (string)query.ExecuteScalar();

                return resultado;



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //delete
        public void Delete(Cliente cliente)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "DELETE FROM Cliente where (id_cli = @id)";

                    query.Parameters.AddWithValue("@id", cliente.Id);

                    var result = query.ExecuteNonQuery();

                    if (result == 0)
                    {
                        MessageBox.Show("Erro ao remover o cliente. Verifique e tente novamente.");
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

        //update
        public void Update(Cliente cliente)
        {
            try
            {
                long enderecoId = cliente.Endereco.Id;
                var endDAO = new EnderecoDAO();

                if (enderecoId > 0)
                    endDAO.Update(cliente.Endereco);
                else
                    enderecoId = endDAO.Insert(cliente.Endereco);

                var query = conn.Query();
                query.CommandText = "UPDATE funcionario SET nome_cli = @nome, cpf_cli = @cpf, datanasc_cli = @datanasc, " +
                                     "contato_cli = @contato " +
                                     "cod_end_fk = @enderecoId WHERE cod_cli = @id";



                query.Parameters.AddWithValue("@nome", cliente.Nome);
                query.Parameters.AddWithValue("@cpf", cliente.Cpf);
                query.Parameters.AddWithValue("@data_nasc", cliente.DataNasc?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@contato", cliente.Contato);
                query.Parameters.AddWithValue("@id_end", enderecoId);


                query.Parameters.AddWithValue("@id", cliente.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("Atualização do registro não foi realizada.");

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }






        public Cliente GetById(int id)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM Cliente where (id_cli = @id)";
                    query.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = query.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Nenhum cliente foi encontrado!");
                            return null;
                        }

                        var cliente = new Cliente();

                        while (reader.Read())
                        {
                            cliente.Id = DAOHelper.GetInt(reader, "id_cli");
                            cliente.Nome = DAOHelper.GetString(reader, "nome_cli");
                            cliente.Cpf = DAOHelper.GetString(reader, "cpf_cli");
                            cliente.DataNasc = DAOHelper.GetDateTime(reader, "data_nascimento_cli");
                            cliente.Contato = DAOHelper.GetString(reader, "contato_cli");
                            cliente.Endereco = new EnderecoDAO().GetById(DAOHelper.GetInt(reader, "id_end_fk"));
                        }

                        return cliente;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }





    }
}
