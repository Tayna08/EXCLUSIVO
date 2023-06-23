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
                query.CommandText = $"INSERT INTO Funcionario (cidade_fun, funcao_fun, complemento_fun, cpf_fun, salario_fun, rg_fun, nome_fun, contato_fun, bairro_fun, rua_fun, cep_fun, numero_fun, data_nasc_fun) " +
                    $"VALUES (@cidade, @funcao, @complemento, @cpf, @salario, @rg, @nome, @contato, @bairro, @rua, @cep, @numero, @data_nasc)";


                query.Parameters.AddWithValue("@cidade", funcionario.Cidade);
                query.Parameters.AddWithValue("@funcao", funcionario.Funcao);
                query.Parameters.AddWithValue("@complemento", funcionario.Complemento);
                query.Parameters.AddWithValue("@cpf", funcionario.Cpf);
                query.Parameters.AddWithValue("@salario", funcionario.Salario);
                query.Parameters.AddWithValue("@rg", funcionario.Rg);
                query.Parameters.AddWithValue("@nome", funcionario.Nome);
                query.Parameters.AddWithValue("@contato", funcionario.Contato);
                query.Parameters.AddWithValue("@bairro", funcionario.Bairro);
                query.Parameters.AddWithValue("@rua", funcionario.Rua);
                query.Parameters.AddWithValue("@cep", funcionario.Cep);
                query.Parameters.AddWithValue("@numero", funcionario.Numero);
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
