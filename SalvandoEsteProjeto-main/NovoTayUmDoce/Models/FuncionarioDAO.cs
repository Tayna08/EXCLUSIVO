﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TayUmDoceProjeto.Conexão;
using System.Windows;

namespace NovoTayUmDoce.Models
{
    class FuncionarioDAO
    {
        private static Conexao conn;

        public FuncionarioDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Funcionario funcionario)
        {
            try
            {

                var enderecoId = new EnderecoDAO().Insert(funcionario.Endereco);

                var query = conn.Query();

                query.CommandText = $"INSERT INTO Funcionario (nome_fun, data_nascimento_fun, cpf_fun, contato_fun, funcao_fun, email_fun, salario_fun, id_end_fk) " +
                    $"VALUES (@nome, @data_nasc, @cpf, @contato, @funcao, @email, @salario, @id_end)";


                query.Parameters.AddWithValue("@funcao", funcionario.Funcao);
                query.Parameters.AddWithValue("@cpf", funcionario.Cpf);
                query.Parameters.AddWithValue("@salario", funcionario.Salario);
                query.Parameters.AddWithValue("@rg", funcionario.Rg);
                query.Parameters.AddWithValue("@nome", funcionario.Nome);
                query.Parameters.AddWithValue("@contato", funcionario.Contato);
                query.Parameters.AddWithValue("@data_nasc", funcionario.Data?.ToString("yyyy-MM-dd"));

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
    }

}
