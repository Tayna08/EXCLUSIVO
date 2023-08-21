using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TayUmDoceProjeto.Conexão;
using System.Windows;



namespace NovoTayUmDoce.Models
{
    class FornecedorDAO
    {
        private static Conexao conn;


        public FornecedorDAO()
        {
            conn = new Conexao();
        }


        public void Insert(Fornecedor fornecedor)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = $"INSERT INTO Fornecedor (nome_fantasia_for, nome_Representante_for, contato_for, cnpj_for, razao_social_for, @bairro, @cidade, @complemento, @rua, @numero)" +
                    $"VALUES (@nome_fantasia, @nome_fornecedor, @contato, @nome_representante, @cnpj, @razao_social, @bairro, @cidade, @complemento, @rua, @numero)";

                query.Parameters.AddWithValue("@nome_fantasia", fornecedor.Nome_Fantasia);
                query.Parameters.AddWithValue("@nome_fornecedor", fornecedor.Nome_Representante);
                query.Parameters.AddWithValue("@contato", fornecedor.Contato);
                query.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
                query.Parameters.AddWithValue("@numero", fornecedor.Razao_Social);
                query.Parameters.AddWithValue("@bairro", fornecedor.Bairro);
                query.Parameters.AddWithValue("@cidade", fornecedor.Cidade);
                query.Parameters.AddWithValue("@complemento", fornecedor.Complemento);
                query.Parameters.AddWithValue("@rua", fornecedor.Rua);
                query.Parameters.AddWithValue("@numero", fornecedor.Numero);

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
