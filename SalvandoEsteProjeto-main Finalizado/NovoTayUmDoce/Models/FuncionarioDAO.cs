﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Windows;
using NovoTayUmDoce.Helpers;
using NovoTayUmDoce.Conexão;

namespace NovoTayUmDoce.Models
{
    class FuncionarioDAO
    {
        private Conexao conn;

        public FuncionarioDAO()
        {
            conn = new Conexao();
        }

        public Funcionario GetById(int id)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM Funcionario WHERE (id_fun = @id)";
                    query.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = query.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Nenhum funcionário foi encontrado!");
                            return null;
                        }

                        var funcionario = new Funcionario();

                        while (reader.Read())
                        {
                            funcionario.Id = DAOHelper.GetInt(reader, "id_fun");
                            funcionario.Nome = DAOHelper.GetString(reader, "nome_fun");
                            funcionario.Cpf = DAOHelper.GetString(reader, "cpf_fun");
                            funcionario.Data = DAOHelper.GetDateTime(reader, "data_nascimento_fun");
                            funcionario.Contato = DAOHelper.GetString(reader, "contato_fun");
                            funcionario.Email = DAOHelper.GetString(reader, "email_fun");
                            funcionario.Funcao = DAOHelper.GetString(reader, "funcao_fun");
                            funcionario.Salario = DAOHelper.GetString(reader, "salario_fun");
                            funcionario.Endereco = new EnderecoDAO().GetById(DAOHelper.GetInt(reader, "id_end_fk"));
                        }

                        return funcionario;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public List<Funcionario> List()
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "SELECT * FROM funcionario LEFT JOIN endereco ON id_end = id_end_fk";
                    using (var reader = query.ExecuteReader())
                    {
                        var lista = new List<Funcionario>();

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

                            lista.Add(funcionario);
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

        public string Insert(Funcionario funcionario)
        {
            try
            {
                var enderecoId = new EnderecoDAO().Insert(funcionario.Endereco);
                var query = conn.Query();
                query.CommandText = "INSERT INTO funcionario (nome_fun, data_nascimento_fun, cpf_fun, contato_fun, funcao_fun, email_fun, salario_fun, id_end_fk) " +
                            "VALUES (@nome, @data_nasc, @cpf, @contato, @funcao, @email, @salario, @id_end)";

                query.Parameters.AddWithValue("@nome", funcionario.Nome);
                query.Parameters.AddWithValue("@data_nasc", funcionario.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@cpf", funcionario.Cpf);
                query.Parameters.AddWithValue("@contato", funcionario.Contato);
                query.Parameters.AddWithValue("@funcao", funcionario.Funcao);
                query.Parameters.AddWithValue("@email", funcionario.Email);
                query.Parameters.AddWithValue("@salario", funcionario.Salario);
                query.Parameters.AddWithValue("@id_end", enderecoId);

                var resultado = (string)query.ExecuteScalar();
                return resultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir os dados: " + ex.Message);
                throw;
            }
        }

        public void Delete(Funcionario funcionario)
        {
            try
            {
                using (var query = conn.Query())
                {
                    query.CommandText = "DELETE FROM Funcionario WHERE (id_fun = @id)";

                    query.Parameters.AddWithValue("@id", funcionario.Id);

                    var result = query.ExecuteNonQuery();

                    if (result == 0)
                    {
                        MessageBox.Show("Erro ao remover o funcionário. Verifique e tente novamente.");
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


        public void Update(Funcionario t)
        {
            try
            {
                long enderecoId = t.Endereco.Id;
                var endDAO = new EnderecoDAO();

                if (enderecoId > 0)
                    endDAO.Update(t.Endereco);
                else
                    enderecoId = endDAO.Insert(t.Endereco);

                var query = conn.Query();
                query.CommandText = "UPDATE Funcionario SET nome_fun = @nome, data_nascimento_fun = @data_nascimento, cpf_fun = @cpf, " +
                    "contato_fun = @contato, funcao_fun = @funcao, email_fun = @email, salario_fun = @salario, " +
                    "id_end_fk = @enderecoId WHERE id_fun = @id";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@data_nascimento", t.Data?.ToString("yyyy-MM-dd")); //"10/11/1990" -> "1990-11-10"
                query.Parameters.AddWithValue("@cpf", t.Cpf);
                query.Parameters.AddWithValue("@contato", t.Contato);
                query.Parameters.AddWithValue("@funcao", t.Funcao);
                query.Parameters.AddWithValue("@email", t.Email);
                query.Parameters.AddWithValue("@salario", t.Salario);
                query.Parameters.AddWithValue("@enderecoId", enderecoId);

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("Atualização do registro não foi realizada.");

            }
            catch (Exception e)
            {
                MessageBox.Show($"Erro ao atualizar funcionario: {e.Message}");
               
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
