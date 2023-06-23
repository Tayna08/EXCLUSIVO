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
                query.CommandText = $"INSERT INTO Fornecedor (nome_fantasia_for, nome_fornecedor_for, contato_for, bairro_for, cidade_for, complemento_for, rua_for, cnpj_for, numero_for, id_est_fk, id_fun_fk)" +
                    $"VALUES (@nome_fantasia, @nome_fornecedor, @contato, @bairro, @cidade, @complemento, @rua, @cnpj, @numero, @id_est, @id_fun)";

                query.Parameters.AddWithValue("@nome_fantasia", fornecedor.Nome_Fantasia);
                query.Parameters.AddWithValue("@nome_fornecedor", fornecedor.Nome_Fornecedor);
                query.Parameters.AddWithValue("@contato", fornecedor.Contato);
                query.Parameters.AddWithValue("@bairro", fornecedor.Bairro);
                query.Parameters.AddWithValue("@cidade", fornecedor.Cidade);
                query.Parameters.AddWithValue("@complemento", fornecedor.Complemento);
                query.Parameters.AddWithValue("@rua", fornecedor.Rua);
                query.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
                query.Parameters.AddWithValue("@numero", fornecedor.Numero);
                query.Parameters.AddWithValue("@id_est", fornecedor.Nome_estoque);
                query.Parameters.AddWithValue("@id_fun", fornecedor.Nome_funcionario);

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
