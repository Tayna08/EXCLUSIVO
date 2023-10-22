using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TayUmDoceProjeto.Conexão;
using System.Windows;
using TayUmDoceProjeto.Models;

namespace NovoTayUmDoce.Models
{
    class FuncionarioDAO
    {
        private static Conexao conn;

        public FuncionarioDAO()
        {
            conn = new Conexao();
        }

        public List<Funcionario> List()
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "SELECT * FROM funcionario LEFT JOIN endereco ON id_end = id_end_fk";
                var reader = query.ExecuteReader();

                var lista = new List<Funcionario>();

                while (reader.Read())
                {

                    var endereco = new Endereco()
                    {
                        Id = reader.GetInt32("id_end"),
                        Bairro = reader.GetString("bairro_end"),
                        Cidade = reader.GetString("cidade_end"),
                        Rua = reader.GetString("rua_end"),
                        Complemento = reader.GetString("complemento_end"),
                        Numero = reader.GetInt32("numero_end"),
                        Cep = reader.GetString("cep_end")
                    };

                    var funcionario = new Funcionario()
                    {
                        Id = reader.GetInt32("id_fun"),
                        Nome = reader.GetString("nome_fun"),
                        Data = reader.GetDateTime("data_nascimento_fun"),
                        Cpf = reader.GetString("cpf_fun"),
                        Contato = reader.GetString("contato_fun"),
                        Funcao = reader.GetString("funcao_fun"),
                        Email = reader.GetString("email_fun"),
                        Salario = reader.GetString("salario_fun"),
                        Endereco = endereco,
                    };

                    lista.Add(funcionario);
                }

                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void Insert(Funcionario funcionario)
        {
            try
            {

                var enderecoId = new EnderecoDAO().Insert(funcionario.Endereco);

                var query = conn.Query();

                query.CommandText = $"INSERT INTO funcionario (nome_fun, data_nascimento_fun, cpf_fun, contato_fun, funcao_fun, email_fun, salario_fun, id_end_fk) " +
                    $"VALUES (@nome, @data_nasc, @cpf, @contato, @funcao, @email, @salario, @id_end)";

                query.Parameters.AddWithValue("@nome", funcionario.Nome);
                query.Parameters.AddWithValue("@data_nasc", funcionario.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@cpf", funcionario.Cpf);
                query.Parameters.AddWithValue("@contato", funcionario.Contato);
                query.Parameters.AddWithValue("@funcao", funcionario.Funcao);
                query.Parameters.AddWithValue("@email", funcionario.Email);
                query.Parameters.AddWithValue("@salario", funcionario.Salario);
                query.Parameters.AddWithValue("@id_end", enderecoId);

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
            
        }

        public void Delete(Funcionario funcionario)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "delete from Funcionário where (id_fun = @id)";

                query.Parameters.AddWithValue("@id", funcionario.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover o funcionário. Verifique e tente novamente.");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}
