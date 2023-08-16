using System;
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

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Funcionario ( nome_fun, data_nasc_fun, cpf_fun, contato_fun, funcao_fun, email_fun, salario_fun, bairro_fun, cidade_fun, rua_fun, complemento_fun, numero_fun) " +
                    $"VALUES (@nome, @data_nasc, @cidade, @cpf, @contato, @funcao, @email, @salario, @bairro, @cidade, @rua, @complemento, @numero)";


                query.Parameters.AddWithValue("@nome", funcionario.Nome);
                query.Parameters.AddWithValue("@data_nasc", funcionario.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@cpf", funcionario.Cpf);
                query.Parameters.AddWithValue("@contato", funcionario.Contato);
                query.Parameters.AddWithValue("@funcao", funcionario.Funcao);
                query.Parameters.AddWithValue("@email", funcionario.Email);
                query.Parameters.AddWithValue("@salario", funcionario.Salario);
                query.Parameters.AddWithValue("@bairro", funcionario.Bairro);
                query.Parameters.AddWithValue("@cidade", funcionario.Cidade); 
                query.Parameters.AddWithValue("@rua", funcionario.Rua);
                query.Parameters.AddWithValue("@complemento", funcionario.Complemento);   
                query.Parameters.AddWithValue("@numero", funcionario.Numero);

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
